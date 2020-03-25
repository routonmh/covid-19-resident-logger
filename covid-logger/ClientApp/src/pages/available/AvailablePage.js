import React from 'react';
import axios from 'axios';

export class AvailablePage extends React.Component {

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

        let req = axios.get(`/api/reports/available?${params.toString()}`);
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
                        </tr>
                    )}
                    </tbody>
                </table>
            );
    }

    render() {
        return (
            <div>
                <h1>Available</h1>
                <p>Viewing residents currently home awaiting assignment ({this.state.residents ? this.state.residents.length : 0})</p>
                {this.renderResidentsTable(this.state.residents)}
            </div>
        )
    }
}