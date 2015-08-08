function Solve(_solutions) {
  var timer = 0;
  for (var i=0; i<50; i++) {
    var moves = _solutions[i].Moves;
    var firstIndex = moves[0];
    var lastIndex = moves[moves.length-1];
    DelegateEvent("mousedown", i, firstIndex, timer++);
    for (var j=0; j<moves.length; j++) {
      DelegateEvent("mouseover", i, moves[j], timer++); 
    }  
    DelegateEvent("mouseup", i, lastIndex, timer++);
    timer += 10;
  }
}

function DelegateEvent(eventName, solutionId, indexId, timer) {
  setTimeout(
    function() {
      console.log("Simulate solution " + solutionId + " field " + indexId + " timer " + timer);
      simulate($("#" + indexId)[0], eventName);    
    },
    timer * 50 + 1000
  );
}

function simulate(element, eventName)
{
    var options = extend(defaultOptions, arguments[2] || {});
    var oEvent, eventType = null;

    for (var name in eventMatchers)
    {
        if (eventMatchers[name].test(eventName)) { eventType = name; break; }
    }

    if (!eventType)
        throw new SyntaxError('Only HTMLEvents and MouseEvents interfaces are supported');

    if (document.createEvent)
    {
        oEvent = document.createEvent(eventType);
        if (eventType == 'HTMLEvents')
        {
            oEvent.initEvent(eventName, options.bubbles, options.cancelable);
        }
        else
        {
            oEvent.initMouseEvent(eventName, options.bubbles, options.cancelable, document.defaultView,
            options.button, options.pointerX, options.pointerY, options.pointerX, options.pointerY,
            options.ctrlKey, options.altKey, options.shiftKey, options.metaKey, options.button, element);
        }
        element.dispatchEvent(oEvent);
    }
    else
    {
        options.clientX = options.pointerX;
        options.clientY = options.pointerY;
        var evt = document.createEventObject();
        oEvent = extend(evt, options);
        element.fireEvent('on' + eventName, oEvent);
    }
    return element;
}

function extend(destination, source) {
    for (var property in source)
      destination[property] = source[property];
    return destination;
}

var eventMatchers = {
    'HTMLEvents': /^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|roll)$/,
    'MouseEvents': /^(?:click|dblclick|mouse(?:down|up|over|move|out))$/
}
var defaultOptions = {
    pointerX: 0,
    pointerY: 0,
    button: 0,
    ctrlKey: false,
    altKey: false,
    shiftKey: false,
    metaKey: false,
    bubbles: true,
    cancelable: true
}