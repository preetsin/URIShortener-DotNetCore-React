import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
//import { Redirect } from 'react-router';

class Direct extends Component {

    constructor(props) {
        super(props);
        this.state = { redirect: '', isLoading: true };
    }

    componentDidMount() {
        let token = this.props.match.params.token;
        fetch(`/api/URIShortener/${token}`)
            .then(res => {
                if (!res.ok) {
                    Promise.reject('Bad response from server');
                }
                return res.json();

            }).then(json => {
                this.setState({
                    redirect: json.uri ? json.uri : '',
                    isLoading: false
                });
                
            }).catch(error => {
                //console.log(`some error ${error}`);
            });
    }

    

    render() {
        
        if (!this.state.isLoading) {
            if (this.state.redirect) {
                window.location.href = this.state.redirect;
                //return <Redirect to={this.state.redirect} />;
            }
            else {
                return (
                    <div>
                        <h1 className="has-error">Invalid Redirect URI <NavLink to="/">Go to home</NavLink> </h1>
                    </div>
                );
            }
        }
        return <h2>Redirecting...</h2>;
    }

}

export default Direct;