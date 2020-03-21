import React, {Component} from 'react';

export class ResidentsPage extends Component {
    displayName = "ResidentsPage";

    constructor(props) {
        super(props);

        let params = new URLSearchParams(window.location.search);
        let residentId = params.get('residentId');
        console.log("residentId: " + residentId);

        let firstName = "";
        let lastName = "";
        let pgy = "";
        let dateOfSymptoms = new Date().toLocaleDateString();
        let symptomsDescription = "";
        let isQuarantined = false;


        this.state = {
            residentId,
            firstName,
            lastName,
            pgy,
            dateOfSymptoms,
            symptomsDescription,
            isQuarantined,


            residentIdDisplayText: "(New)",

        }
    }

    componentDidMount() {
        document.title = "Resident Log";
    }

    render() {
        return (
            <div>
                <h1>Resident</h1>

                <p>Enter resident information:</p>

                <div className="inline-button-container">
                    <div className="inline-button-container">
                        <button type="button" className="btn btn-primary btn-sm"
                                style={{marginRight: "1rem"}}
                                onClick={(e) => e.target.blur()}>Previous
                        </button>
                        <button type="button" className="btn btn-primary">Next</button>
                    </div>

                    <button type="button" className="btn btn-success btn-sm">New</button>
                </div>

                <hr className="half-rule"/>

                <form>
                    <div className="form-group">
                        <label>ResidentID: {this.state.residentIdDisplayText}</label>
                    </div>

                    <div className="form-group-inline-flex">
                        <div className="form-group inline-group-item ">
                            <label>First Name:</label>
                            <input type="text" className="form-control"
                                   maxLength={32}
                                   size={32}
                                   onChange={e => this.setState({firstName: e.target.value})}
                                   placeholder="John"/>
                        </div>

                        <div className="form-group inline-group-item ">
                            <label>Last Name:</label>
                            <input type="text" className="form-control"
                                   maxLength={32}
                                   size={32}
                                   placeholder="Doe"/>
                        </div>

                    </div>

                    <div className="form-group">
                        <label>PGY:</label>
                        <input type="text" className="form-control"
                               size={5}
                               maxLength={5}
                               placeholder="PGY-"/>
                    </div>

                    <div className="form-group">
                        <label>Phone Number:</label>
                        <input type="text" className="form-control"
                               maxLength={16}
                               size={16}
                               placeholder="(123) 456-7890"/>
                    </div>

                    <div className="form-group">
                        <label>Date of Symptoms:</label>
                        <input type="date" className="form-control"/>
                    </div>

                    <div className="form-group">
                        <label>Symptoms Description:</label>
                        <textarea className="form-control" rows="3"
                                  placeholder="Description"></textarea>
                    </div>

                    <div className="form-group form-group-inline-flex">

                        <div className="form-group inline-group-item">
                            <label>Date of COVID-19 Testing:</label>
                            <input type="date" className="form-control"/>
                        </div>

                        <div className="form-group inline-group-item">
                            <label>Result of COVID-19 Test:</label>
                            <select className="form-control">
                                <option>Not Tested</option>
                                <option>Pending</option>
                                <option>Negative</option>
                                <option>Positive</option>
                            </select>
                        </div>

                    </div>

                    <div className="form-group">
                        <label>Quarantine Status:</label>

                        <div className="form-group form-group-inline-flex">
                            <div className="form-check inline-radio-group-item">
                                <input className="form-check-input " type="radio"
                                       value="Not Quarantined"
                                       checked={!this.state.isQuarantined}/>
                                <label className="form-check-label">
                                    Not Quarantined
                                </label>
                            </div>
                            <div className="form-check inline-radio-group-item">
                                <input className="form-check-input " type="radio"
                                       value="Quarantined"
                                       checked={this.state.isQuarantined}/>
                                <label className="form-check-label">
                                    Quarantined
                                </label>
                            </div>
                        </div>
                    </div>

                    <div className="form-group form-group-inline-flex">

                        <div className="form-group inline-group-item">
                            <label>Quarantined Until:</label>
                            <input type="date" className="form-control"/>
                        </div>

                    </div>

                    <button type="button" className="btn btn-primary btn-sm">Save</button>
                </form>

                <hr className="half-rule"/>

                <h1>Duty Assignment</h1>

                <p>Assign resident duty:</p>

                <form>
                    <div className="form-group">

                        <div className="form-group-inline-flex">
                            <div className="form-group inline-group-item">
                                <label>Duty:</label>
                                <select className="form-control">
                                    <option>Home Awaiting Assignment</option>
                                    <option>Floor</option>
                                    <option>ICU Day</option>
                                    <option>ICU Night</option>
                                    <option>COVID</option>
                                </select>
                            </div>

                            <div className="form-group inline-group-item">
                                <label>Start Date:</label>
                                <input type="date" className="form-control"/>
                            </div>

                            <div className="form-group inline-group-item">
                                <label>End Date:</label>
                                <input type="date" className="form-control"/>
                            </div>
                        </div>

                        <button type="button" className="btn btn-primary btn-sm">Assign</button>
                    </div>
                </form>
            </div>
        );
    }
}
