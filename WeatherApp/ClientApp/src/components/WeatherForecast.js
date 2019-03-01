import React, { Component } from 'react';

export class WeatherForecast extends Component
{
    static displayName = WeatherForecast.name;

    constructor(props)
    {
        super(props);
        this.state = { forecasts: [], loading: true };

        //fetch('api/City/name/Warszawa/7')
        //    .then(response => response.json())
        //    .then(data => {
        //        this.setState({ forecasts: data, loading: false });
        //    });
        this.findForecastRecords = this.findForecastRecords.bind(this);
        this.updateInputValue = this.updateInputValue.bind(this);
    }

    static renderForecastsTable(data)
    {
        debugger
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temperature (C)</th>
                        <th>Humidity</th>
                        <th>Snow</th>
                        <th>Rain</th>
                        <th>Wind</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map(d =>
                        <tr key={d.measureDate}>
                            <td>{d.measureDate}</td>
                            <td>{d.temperature}</td>
                            <td>{d.humidity}</td>
                            <td>{d.snow}</td>
                            <td>{d.rain}</td>
                            <td>{d.wind}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    findForecastRecords()
    {
        var that = this;
        this.setState({ loading: true });
        var cityName = this.state.inputValue;
        fetch('api/City/name/' + cityName +'/7')
            .then(response => response.json())
            .then(data => {
                that.setState({ forecasts: data, loading: false });
                debugger;
            });
    }

    updateInputValue(evt)
    {
        debugger
        this.setState({ inputValue: evt.target.value });
    }

    render()
    {
        let contents = this.state.loading ? <p><em>No data</em></p> : WeatherForecast.renderForecastsTable(this.state.forecasts.weatherMeasures);

        return (
            <div>
                <input value={this.state.inputValue} onChange={this.updateInputValue} />
                <button className="btn btn-primary" onClick={this.findForecastRecords}>Find</button>
                <h3>Weather forecast</h3>
                {contents}
            </div>
        );
    }
}
