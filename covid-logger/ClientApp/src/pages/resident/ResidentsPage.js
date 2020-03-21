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

                <p>Enter resident information here.</p>

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
                        <div className="form-group inline-text-group-item ">
                            <label>First Name:</label>
                            <input type="text" className="form-control"
                                   maxLength={32}
                                   size={32}
                                   placeholder="John"/>
                        </div>

                        <div className="form-group inline-text-group-item ">
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
                               placeholder="PGY-"/>
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

                    <div className="form-group">
                        <label>Date of COVID-19 Testing:</label>
                        <input type="date" className="form-control"/>
                    </div>

                    <div className="form-group">
                        <label>Result of COVID-19 Test:</label>
                        <select className="form-control">
                            <option>Not Tested</option>
                            <option>Pending</option>
                            <option>Negative</option>
                            <option>Positive</option>
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Quarantine Status:</label>

                        <div className="form-group-inline-flex">
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
                </form>

                <hr className="half-rule"/>

                <h1>Duty Assignment</h1>

                <p>Enter duty assignment:</p>

            </div>
        );
    }
}
