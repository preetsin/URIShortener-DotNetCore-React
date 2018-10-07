import React, { Component } from 'react';
import { ShortUriForm } from './ShortUriForm';

export class Home extends Component {
  
  render() {
      return (
        <div className="home">
            <h1>Short URI</h1>
            <ShortUriForm />
        </div>  
    );
  }
}
