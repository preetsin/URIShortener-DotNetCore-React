import React, { Component, Fragment } from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import ShortUriForm from './components/ShortUriForm';
import Direct from './components/Direct';

export default class App extends Component {

    render() {
        return (
            <BrowserRouter>
                <Switch>
                    <div className="container">
                        <h1>URI Shortener</h1>
                        <Fragment>
                            <Route path='/' exact component={ShortUriForm} />
                        </Fragment>
                        <Fragment>
                            <Route path='/:token' exact component={Direct} />
                        </Fragment>
                    </div>
                </Switch>
            </BrowserRouter>
    );
  }
}
