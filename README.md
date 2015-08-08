# Słowotok
to aplikacja wyszukująca rozwiązania w grze http://slowotok.pl. Została napisana w C# i działa na platformie Windows.
Ponadto zawiera skrypt, umożliwiający półautomatyczne wpisanie rozwiązań w wersji przeglądarkowej gry, przetestowane na Google Chrome.

## Instalacja
Pobierz najnowszą stabilną wersję aplikacji https://github.com/mtosny/slowotok/releases/latest i wypakuj.

## Wyszukiwanie rozwiązań
1. wejdź do folderu programu i uruchom *Slowotok.exe*
2. wpisz 16 liter występujących w grze (wierszami od lewej do prawej) i wciśnij ENTER
3. po chwili na ekranie konsoli pojawi się 50 najdłuższych znalezionych słów

Dla przykładowej gry z poniższego obrazka należy w konsoli wpisać ciąg "*ósauiłkćczrpłeżp*"

![alt tag](https://raw.github.com/mtosny/slowotok/master/img/game_board.png)

na co rozwiązanie jakie pojawi się w konsoli będzie wyglądać następująco:

![alt tag](https://raw.github.com/mtosny/slowotok/master/img/game_console.png)

Znalezione rozwiązania można wprowadzić do gry ręcznie lub wykorzystać skrypt opisany niżej.

##Automatyczne wpisanie rozwiązań na stronie www
1. otwórz w edytorze tekstu plik *bot-script.js* i skopiuj jego zawartość
2. w przeglądarce www otwórz konsole deweloperską (F12), wklej zawartość pliku i zatwierdź ENTERem
3. wykonaj kroki z sekcji [Wyszukiwanie rozwiązań](#wyszukiwanie-rozwiązań)
4. otwórz plik *solutions_<16-liter-z-gry>.txt* i skopiuj jego zawartość
5. w konsoli deweloperskiej przeglądarki wydaj polecenie *Solve( tutaj wklej zawartość pliku )* i wciśnij ENTER
6. w oknie przeglądarki rozwiązania będą się wpisywać same, czego skutkiem jest taki ranking :)

![alt tag](https://raw.github.com/mtosny/slowotok/master/img/game_ranking.png)

## Disclaimer
Aplikacja została napisana w celach hobbystycznych. Powinno się traktować ją jako ciekawostkę, a nie narzędzie do manipulowania wynikami w regularnych grach w grze Słowotok.
