import React, {Component} from 'react';
import axios from 'axios';
import dateformat from 'dateformat';

export class ResidentEntry extends Component {
    displayName = "ResidentsPage";

    constructor(props) {
        super(props);

        let params = new URLSearchParams(window.location.search);
        let idInParams = params.get('residentId');
        let creatingNewResident = !idInParams;
        let residentId = creatingNewResident ? "" : idInParams;

        let firstName = "";
        let lastName = "";
        let pgy = "";
        let phoneNumber = "";
        let symptomsDate = "";
        let symptomsDescription = "";
        let covid19TestDate = "";
        let covid19TestResult = "";
        let isQuarantined = false;
        let quarantinedUntil = "";

        this.state = {
            creatingNewResident,

            residentId,
            firstName,
            lastName,
            pgy,
            phoneNumber,
            symptomsDate,
            symptomsDescription,
            covid19TestDate,
            covid19TestResult,
            isQuarantined,
            quarantinedUntil,

            testResultOptions: [],
            dutyOptions: [],

            selectedDutyOption: "0",
            dutyStartDate: "",
            dutyEndDate: "",

            previousId: -1,
            nextId: -1
        };

        this.canSubmitResidentChanges = this.canSubmitResidentChanges.bind(this);
        this.submitResidentChanges = this.submitResidentChanges.bind(this);
        this.canSubmitResidentChanges = this.canSubmitResidentChanges.bind(this);
        this.submitAssignment = this.submitAssignment.bind(this);
    }

    componentDidMount() {

        document.title = "Resident Log";

        this.fetchInitials();
    }

    async fetchInitials() {

        if (!this.state.creatingNewResident)
            await this.fetchResident();

        this.fetchTestResultOptions();
        this.fetchDutyOptions();
        this.fetchLocalIDSet();
    }

    async fetchResident() {
        try {
            let req = axios.get(`/api/resident/${this.state.residentId}`);
            let res = await req;

            let residentFields = {...res.data};

            residentFields.symptomsDate = dateformat(residentFields.symptomsDate, "isoDate");
            residentFields.covid19TestDate = dateformat(residentFields.covid19TestDate, "isoDate");
            residentFields.quarantinedUntil = dateformat(residentFields.quarantinedUntil, "isoDate");

            this.setState({...residentFields});
        } catch (ex) {
            alert("ResidentID does not exist.");
        }
    }

    async fetchTestResultOptions() {

        let req = axios.get('/api/resident/test-result-types');
        let res = await req;
        this.setState({testResultOptions: res.data})
    }

    async fetchDutyOptions() {

        let req = axios.get('/api/duty/duty-types');
        let res = await req;
        this.setState({dutyOptions: res.data})
    }

    async fetchLocalIDSet() {

        if (this.state.residentId !== "") {
            let req = axios.get(`/api/resident/local-id-set/${this.state.residentId}`);
            let res = await req;

            let localSet = res.data;

            this.setState({
                previousId: localSet.previousID,
                nextId: localSet.nextID
            })
        }
    }

    async submitResidentChanges() {

        let fields = {
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            pgy: this.state.pgy,
            phoneNumber: this.state.phoneNumber,
            symptomsDate: this.state.symptomsDate,
            symptomsDescription: this.state.symptomsDescription,
            covid19TestDate: this.state.covid19TestDate,
            covid19TestResult: this.state.covid19TestResult === "" ?
                0 : this.state.covid19TestResult,
            isQuarantined: this.state.isQuarantined,
            quarantinedUntil: this.state.quarantinedUntil
        };

        if (this.state.creatingNewResident) {
            try {
                let req = axios.post('/api/resident', {
                    ...fields
                });
                let res = await req;

                this.setState({
                    residentId: res.data.residentID,
                    creatingNewResident: false
                }, () => alert("Resident record was created."))
            } catch (ex) {
                alert('There was an error creating the record.');
            }
        } else {
            try {
                let req = axios.put(`/api/resident/${this.state.residentId}`, {
                    ...fields
                });
                await req;

                alert("Resident record was updated.");
            } catch (ex) {
                alert("There was an error updating the record.")
            }
        }
    }

    async submitAssignment() {

        try {
            let req = axios.post('/api/duty/assign', {
                residentId: this.state.residentId,
                dutyType: this.state.selectedDutyOption,
                dateStart: this.state.dutyStartDate,
                dateEnd: this.state.dutyEndDate
            });
            await req;

            alert("Resident was assigned duty");
        } catch (ex) {
            alert('There was an error assigning this resident');
        }
    }

    canSubmitResidentChanges() {
        return true;
    }

    canSubmitAssignment() {
        return this.state.dutyEndDate !== "" && this.state.dutyStartDate !== "" &&
            this.state.selectedDutyOption != "" && this.state.residentId != "";
    }

    clearStagedAssignment() {
        this.setState({
            selectedDutyOption: "",
            dutyStartDate: "",
            dutyEndDate: ""
        })
    }

    renderTestResultOptions = () => this.state.testResultOptions
        .map((it, idx) =>
            <option key={`${it.testResultDescription}${idx}`}
                    value={it.testResultType}>{it.testResultDescription}</option>);


    renderDutyOptions = () => this.state.dutyOptions
        .map((it, idx) =>
            <option key={`${it.dutyDescription}${idx}`} value={it.dutyType}>{it.dutyDescription}</option>);


    render() {
        return (
            <div>
                <h1>Data Entry</h1>

                <p>Enter resident information:</p>

                <div className="inline-button-container">
                    <div className="inline-button-container">
                        <button type="button" className="btn btn-primary btn-sm"
                                style={{marginRight: "1rem"}}
                                disabled={this.state.previousId == -1}
                                onClick={(e) => window.location = `?residentId=${this.state.previousId}`}>Previous
                        </button>
                        <button type="button" className="btn btn-primary"
                                disabled={this.state.nextId == -1}
                                onClick={(e) => window.location = `?residentId=${this.state.nextId}`}>Next
                        </button>
                    </div>

                    <button type="button" className="btn btn-success btn-sm"
                            onClick={(e) => {
                                window.location = "/";
                                e.target.blur();
                            }}>New
                    </button>
                </div>

                <hr className="half-rule"/>

                <form>
                    <div className="form-group">
                        <label>ResidentID: {this.state.creatingNewResident ?
                            "(New)" : this.state.residentId}</label>
                    </div>

                    <div className="form-group-inline-flex">
                        <div className="form-group inline-group-item ">
                            <label>First Name:</label>
                            <input type="text" className="form-control"
                                   maxLength={32}
                                   size={32}
                                   value={this.state.firstName}
                                   onChange={e => this.setState({firstName: e.target.value})}
                                   placeholder="John"/>
                        </div>

                        <div className="form-group inline-group-item ">
                            <label>Last Name:</label>
                            <input type="text" className="form-control"
                                   maxLength={32}
                                   size={32}
                                   value={this.state.lastName}
                                   onChange={e => this.setState({lastName: e.target.value})}
                                   placeholder="Doe"/>
                        </div>

                    </div>

                    <div className="form-group">
                        <label>PGY:</label>
                        <input type="text" className="form-control"
                               size={5}
                               maxLength={5}
                               value={this.state.pgy}
                               onChange={e => this.setState({pgy: e.target.value})}
                               placeholder="PGY-"/>
                    </div>

                    <div className="form-group">
                        <label>Phone Number:</label>
                        <input type="text" className="form-control"
                               maxLength={16}
                               size={16}
                               value={this.state.phoneNumber}
                               onChange={e => this.setState({phoneNumber: e.target.value})}
                               placeholder="(123) 456-7890"/>
                    </div>

                    <div className="form-group">
                        <label>Date of Symptoms:</label>
                        <input type="date" className="form-control"
                               value={this.state.symptomsDate}
                               onChange={e => this.setState({symptomsDate: e.target.value})}/>
                    </div>

                    <div className="form-group">
                        <label>Symptoms Description:</label>
                        <textarea className="form-control" rows="3"
                                  value={this.state.symptomsDescription}
                                  onChange={e => this.setState({symptomsDescription: e.target.value})}
                                  placeholder="Description"/>
                    </div>

                    <div className="form-group form-group-inline-flex">

                        <div className="form-group inline-group-item">
                            <label>Date of COVID-19 Testing:</label>
                            <input type="date" className="form-control"
                                   value={this.state.covid19TestDate}
                                   onChange={e => {
                                       let v = e.target.value;
                                       this.setState({covid19TestDate: v});
                                       if (v === "")
                                           this.setState({covid19TestResult: "0"})
                                   }}/>
                        </div>

                        <div className="form-group inline-group-item">
                            <label>Result of COVID-19 Test:</label>
                            <select className="form-control"
                                    value={this.state.covid19TestResult}
                                    disabled={this.state.covid19TestDate === ""}
                                    onChange={e => this.setState({covid19TestResult: e.target.value})}>
                                {this.renderTestResultOptions()}
                            </select>
                        </div>

                    </div>

                    <div className="form-group">
                        <label>Quarantine Status:</label>

                        <div className="form-group form-group-inline-flex">
                            <div className="form-check inline-radio-group-item"
                                 onClick={() => this.setState({isQuarantined: false})}>
                                <input className="form-check-input " type="radio"
                                       onChange={() => {
                                           this.setState({
                                               isQuarantined: false,
                                               quarantinedUntil: ""
                                           });
                                       }}
                                       checked={!this.state.isQuarantined}/>
                                <label className="form-check-label">
                                    Not Quarantined
                                </label>
                            </div>
                            <div className="form-check inline-radio-group-item"
                                 onClick={() => this.setState({isQuarantined: true})}>
                                <input className="form-check-input " type="radio"
                                       onChange={() => this.setState({isQuarantined: true})}
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
                            <input type="date" className="form-control"
                                   disabled={!this.state.isQuarantined}
                                   value={this.state.quarantinedUntil}
                                   onChange={e => this.setState({quarantinedUntil: e.target.value})}/>
                        </div>

                    </div>

                    <button type="button" className="btn btn-primary btn-sm"
                            disabled={!this.canSubmitResidentChanges()}
                            onClick={this.submitResidentChanges}>Save
                    </button>
                </form>

                <hr className="half-rule"/>

                <h1>Duty Assignment</h1>

                <p>Assign resident duty:</p>

                <form>
                    <div className="form-group">

                        <div className="form-group-inline-flex">
                            <div className="form-group inline-group-item">
                                <label>Duty:</label>
                                <select className="form-control"
                                        value={this.state.selectedDutyOption}
                                        onChange={e => this.setState({selectedDutyOption: e.target.value})}>
                                    {this.renderDutyOptions()}
                                </select>
                            </div>

                            <div className="form-group inline-group-item">
                                <label>Start Date:</label>
                                <input type="date" className="form-control"
                                       value={this.state.dutyStartDate}
                                       onChange={e => this.setState({dutyStartDate: e.target.value})}/>
                            </div>

                            <div className="form-group inline-group-item">
                                <label>End Date:</label>
                                <input type="date" className="form-control"
                                       value={this.state.dutyEndDate}
                                       onChange={e => this.setState({dutyEndDate: e.target.value})}/>
                            </div>
                        </div>

                        <button type="button" className="btn btn-primary btn-sm"
                                disabled={!this.canSubmitAssignment()}
                                onClick={async () => {
                                    await this.submitAssignment();
                                    this.clearStagedAssignment();
                                }}>Assign
                        </button>
                    </div>
                </form>
            </div>
        );
    }
}
