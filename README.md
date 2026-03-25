# ćw 2 APBD

instrukcja uruchomienia programu:
```bash
$ dotnet run
```
Po uruchomieniu program wyświetli menu TUI
```
Select option
1. add new user
2. add new device
3. make a new rent
4. return an item
5. display inventory status
6. display available devices
7. display current rents for user
8. display rent history
```
Program wyświetla instrukcję przy każdej opcji.

# Decyzje projektowe
Główną klasą w projekcie jest klasa TUI, w jej kostruktorze należy przekazać listę użytkowników i obiekt klasy RentalService (Dependency Injection), obiekt TUI staje się ich właścicielem i będzie nimi zarządzał do końca działania programu.

Klasy w urządzeń np. Laptop dziedziczą po abstrakcyjnej klasie Device co pozwala na trzymanie różnych urządzeń w jednym kontenerze oraz generowanie numerów ID dla wszystkich urządzeń razem.

Aby zaimplementować różne typy użytkowników powstały klasy EmployeeTier i StudentTier implementujące interface IUserType. Obiekty klasy user przechowują obiekt po interfejsie IUserTier który określa typ użytkownika (w tym ile urządzeń może wypożyczyć), w ten sposób można łatwo zmienić maksymalną ilość urządzeń do wypożyczenia dla danego typu użytkownika.

Klasa TUI podczas wyświetlania formularza dodania nowego sprzentu albo nowego użytkownika używa refleksji aby znaleźć klasy dziedziczące po Device / implementujące IUserType przez co aby nowy typ urządzenia został wyświetlony w formularzy należy poprostu dodać nową klasę.

# Czego nie udało się zrealizować
- Obsługi wszystkich błędów
- Naliczania kary za nie oddanie sprzętu w terminie
- Serializacji stanu wypożyczalni do JSON