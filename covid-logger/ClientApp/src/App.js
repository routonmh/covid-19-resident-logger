import React, {Component} from 'react';
import {Route} from 'react-router';
import {Layout} from './components/Layout';
import {Home} from './pages/Home';
import {FetchData} from './pages/FetchData';
import {Counter} from './pages/Counter';
import { ResidentsPage} from "./pages/resident/ResidentsPage";

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={ResidentsPage} />
                <Route path='/counter' component={Counter}/>
                <Route path='/fetchdata' component={FetchData}/>
            </Layout>
        );
    }
}
