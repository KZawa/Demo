import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Dzien dobry</h1>
        <ul>
          <li>Na tej stronie jest przedstawiona instrukcja obslugi. </li>
          <li>Aby zobaczyc dokumentacje Api trzeba dokleic do adresu tej strony:<strong> /swagger/index.html</strong>.</li>
          <li>Preferowana przegladarka do ogladania dokumentacji Api to Internet Explorer</li>
          <li>W bazie danyh znajduja sie 3 encje: WeatherMeasure, City, User. Encja WeatherMeasure jest w relacji N:1 z encja City. </li>
          <li>Na stronie znajduja sie 2 zakladki: 'Home' i 'Weather Forecast'</li>
          <li>Jak sie wejdzie w zakladke 'Weather Forecast' to zostanie wyswietlona niepelna prognoza pogody</li>
          <li>Po wpisaniu poprawnej nazwy miasta i wcisniecu przycisku 'Find', zostanie wyswietlona prognoza pogody na najblizsze 7 dni (o ile instnieja dane)</li>
          <li>dla nazw : <strong>Warszawa, Londyn, Kapsztad</strong> zostaly wprowadzone dane demonstracyjne.</li>
          <li></li>
          <li>Aby moc wykonywac operacje na bazie danych inne niz get trzeba sie zalogowac jako admin.</li>
          <li>"login": "Admin", "password": "password123"</li>
          <li>Po poprawnym zalogowaniu jest wysylany ze strony  token"</li>
          <li>Aby modyfikowac dane w bazie trzeba dodac naglowek do zadania http <strong> Authorization: Bearer 'otrzymany_token'</strong></li>
          <li></li>
          <li>API</li>
          <li>W bazie danych nie moga istniec dwa rekordy w tabeli WeatherMeasure, ktore maja ta sama date pomiaru i maja ten sam klucz obcy do tabeli Cities</li>
          <li>W bazie danych w tabeli Cities nazwy miast musza byc unikalne</li>
          <li>Date powinno podawac sie w formacie "dd-MM-yyyy"</li>
          <li>Dla komunikatu get dla kontrolera City jest pobierane miasto oraz rekordy typu WeatherMeasure, ktorych daty pomiarowe znajduja sie od daty dzisiejszej az do daty okrslonej za pomoca parametru 'dayCount', </li>


        </ul>
      </div>
    );
  }
}
