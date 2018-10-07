import React, { Component } from 'react';

class ShortUriForm extends Component {

    constructor(props) {
        super(props);
        this.state = {
            uri: '',
            alias: '',
            errorMessage: ''
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleInputChange(event) {
        event.preventDefault();
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    handleSubmit(event) {
        event.preventDefault();
        //console.log(`${JSON.stringify(this.state, null, 2)}`);
        
        fetch('/api/URIShortener', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ 'Uri': this.state.uri })

        }).then(res => {
            if (!res.ok) {
                Promise.reject('Bad response from server');
            }
            return res.json();

        }).then(json => {
            //console.log('Response.Json', json !== undefined ? json : '');
            this.setState({
                alias: json.alias,
                errorMessage: json.Uri
            });

        }).catch(error => {
            //console.log(`some error ${error}`);
            this.setState({ errorMessage: "Some error occured, Please try again later." });
        });
    }
    
    render() {
        let alias = this.state.alias;
        let errMsg = this.state.errorMessage;

        return (
            <form onSubmit={this.handleSubmit}>
                <div className="form-group">
                    <input name="uri" placeholder="enter uri" onChange={this.handleInputChange} />
                    <button className="btn btn-primary">Go</button>
                </div>
                { errMsg ? <p className="has-error">{errMsg}</p> : '' }
                { alias ? <p className="alias">{alias}</p> : '' }
            </form>
        );
    }

}

export default ShortUriForm;