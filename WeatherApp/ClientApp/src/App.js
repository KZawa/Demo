import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { WeatherForecast } from './components/WeatherForecast';
import { Login } from './components/Login';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
            <Route path='/weather-forecast' component={WeatherForecast} />
            <Route path='/login' component={Login} />
      </Layout>
    );
  }
}
