import React from 'react';
import axios from 'axios';

export class AssignmentsPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            fetchingResults: true,
            residents: [],
            sortingField: "DateStart",
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

        let req = axios.get(`/api/reports/assignments?${params.toString()}`);
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
                        <th>Duty</th>
                        <th>Date Start</th>
                        <th>Date End</th>
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
                            <td>{it.dutyDescription}</td>
                            <td>{it.dutyStartDate}</td>
                            <td>{it.dutyEndDate}</td>
                        </tr>
                    )}
                    </tbody>
                </table>
            );
    }

    render() {
        return (
            <div>
                <h1>Assignments</h1>
                <p>Viewing assigned residents ({this.state.residents ? this.state.residents.length : 0})</p>
                {this.renderResidentsTable(this.state.residents)}
            </div>
        )
    }
}