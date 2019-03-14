import React, { Component } from 'react';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props)
    {
        super(props);
        this.state = { isLoggedIn: false, token: ''};

        this.updateLoginValue = this.updateLoginValue.bind(this);
        this.updatePasswordValue = this.updatePasswordValue.bind(this);
        this.RenderLoginForm = this.RenderLoginForm.bind(this);
        this.RenderAdminView = this.RenderAdminView.bind(this);
        this.updateCityValue = this.updateCityValue.bind(this);
        this.GetToken = this.GetToken.bind(this);
        this.logout = this.logout.bind(this);
        this.UpdateDatebase = this.UpdateDatebase.bind(this);
    }

    updateLoginValue(evt) {
        debugger
        this.setState({ loginValue: evt.target.value });
    }

    updatePasswordValue(evt) {
        debugger
        this.setState({ passwordValue: evt.target.value });
    }

    updateCityValue(evt) {
        this.setState({ cityValue: evt.target.value });
    }

    logout(evt) {
        this.setState({ token: '', isLoggedIn: false })
    }

    GetToken(e)
    {
        e.preventDefault();
        var that = this;
        fetch('/api/User/login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                   login: that.state.loginValue,
                   password: that.state.passwordValue,
            })
        })
       .then(response => {
           if (response.ok)
               return response.text();
           else
           {
               throw new Error('Login error');
           }
       })
       .then(data => {
           debugger
           that.setState({ token: data, isLoggedIn: true });
       })
       .catch((error) => {
           debugger
           that.setState({ token: '', isLoggedIn: false });
           console.log(error)
           alert(error);
       });           
    }

    UpdateDatebase()
    {
        var cityName = this.state.cityValue;
        var url = '/api/WeatherMeasurement/cityName/' + cityName;
        fetch(url, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.state.token
            },
            body: JSON.stringify({})
        })
        .then(response => {
                
            if (response.status === 200) {
                alert('Datebase has been updated');
            }
            else
            {
                alert('An unexpected error occurred');
            }
        })
        .catch((error) => {
            debugger
        }); 
    }

    RenderLoginForm()
    {
        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Login:
                    <br />
                    <input type="text" value={this.state.value} onChange={this.updateLoginValue} />
                </label>
                <br />
                <label>
                    Pasword:
                    <br />
                    <input type="password" value={this.state.value} onChange={this.updatePasswordValue} />
                </label>
                <br />
                <button className="btn btn-primary" onClick={(e) => { this.GetToken(e) }}>Login</button>
            </form>
        );
    }

    RenderAdminView()
    {
        return (
            <div>
            <h3>Refresh Data for below city</h3>
            <input value={this.state.inputValue} onChange={this.updateCityValue} />
            <button className="btn btn-primary" onClick={this.UpdateDatebase}>Refresh data</button>
            <br />
            <button className="btn btn-primary" onClick={this.logout}> Logout</button>
            </div>       
        );
    }

    render()
    {
        debugger
        let contents = this.state.isLoggedIn ? this.RenderAdminView() : this.RenderLoginForm();
        return (
            <div>
                {contents}
            </div>
        );
    }
}
