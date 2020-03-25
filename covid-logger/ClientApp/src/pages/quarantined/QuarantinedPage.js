import React from 'react';
import axios from 'axios';

export class QuarantinedPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            fetchingResults: true,
            residents: [],
            sortingField: "ResidentID",
            sortAscending: false
        };

        this.fetchResidents = this.fetchResidents.bind(this);
    }

    componentDidMount() {
        this.fetchResidents();
    }

    async fetchResidents() {

        await this.setState({fetchingResults: true});

        let params = new URLSearchParams({
            sortByFieldName: this.state.sortingField,
            asc: this.state.sortAscending
        });

        let req = axios.get(`/api/reports/quarantined?${params.toString()}`);
        let res = await req;

        this.setState({
            residents: res.data,
            fetchingResults: false
        });
    }

    renderResidentsTable(residents) {

        if (this.state.fetchingResults)
            return (<p>Loading . . .</p>);
        else
            return (
                <table className='table'>
                    <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>PGY</th>
                        <th>Phone Number</th>
                        <th>Date of Symptoms</th>
                        <th>COVID-19 Test Date</th>
                        <th>COVID-19 Test Result</th>
                        <th>Quarantined?</th>
                    </tr>
                    </thead>
                    <tbody>
                    {residents.map((it, idx) =>
                        <tr key={JSON.stringify(it) + `${idx}`}
                            className="clickable-item"
                            onClick={() => {
                                let params = new URLSearchParams({residentId: it.residentID});
                                window.location = `/?${params.toString()}`;
                            }}>
                            <td>{it.firstName}</td>
                            <td>{it.lastName}</td>
                            <td>{it.pgy}</td>
                            <td>{it.phoneNumber}</td>
                            <td>{it.symptomsDate}</td>
                            <td>{it.covid19TestDate}</td>
                            <td>{it.covid19TestResult}</td>
                            <td>{it.isQuarantined}</td>
                        </tr>
                    )}
                    </tbody>
                </table>
            );
    }

    render() {
        return (
            <div>
                <h1>Quarantined Residents</h1>
                <p>Viewing quarantined residents ({this.state.residents ? this.state.residents.length : 0})</p>
                {this.renderResidentsTable(this.state.residents)}
            </div>
        )
    }
}