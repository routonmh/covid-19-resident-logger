import React, {Component} from 'react';
import {Route} from 'react-router';
import {Layout} from './components/Layout';
import {ResidentEntry} from "./pages/residentEntry/ResidentEntry";
import {ResidentsPage} from "./pages/residents/ResidentsPage";
import {QuarantinedPage} from "./pages/quarantined/QuarantinedPage";
import {OnDutyPage} from "./pages/onduty/OnDutyPage";
import {AvailablePage} from "./pages/available/AvailablePage";
import {AssignmentsPage} from "./pages/assignments/AssignmentsPage";

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={ResidentEntry}/>
                <Route exact path='/residents' component={ResidentsPage}/>
                <Route exact path='/quarantined' component={QuarantinedPage}/>
                <Route exact path='/on-duty' component={OnDutyPage}/>
                <Route exact path='/available' component={AvailablePage}/>
                <Route exact path='/assignments' component={AssignmentsPage}/>
            </Layout>
        );
    }
}
