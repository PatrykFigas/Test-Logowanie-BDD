Feature: Logowanie użytkownika

  Scenario: Pomyślne logowanie użytkownika
    Given Użytkownik znajduje się na stronie logowania
    When Wprowadza poprawną nazwę użytkownika i hasło
    And Naciska przycisk "Zaloguj"
    Then Zostaje przekierowany na stronę główną

  Scenario: Logowanie z nieprawidłowym hasłem
    Given Użytkownik znajduje się na stronie logowania
    When Wprowadza poprawną nazwę użytkownika i niepoprawne hasło
    And Naciska przycisk "Zaloguj"
    Then Widzi komunikat "User or Password is not valid"