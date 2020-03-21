import React, {Component} from 'react';

export class ResidentsPage extends Component {
    displayName = "ResidentsPage";

    componentDidMount() {
        document.title = "Resident Log";
    }

    render() {
        return (
            <div>
                <h1>Resident</h1>

                <p>Enter resident information here.</p>

                <form>
                    <div className="form-group">
                        <label>ResidentID: 1</label>
                    </div>

                    <div className="form-group">
                        <label>First Name:</label>
                        <input type="text"  className="form-control"
                               placeholder="John"/>
                    </div>

                    <div className="form-group">
                        <label>Last Name:</label>
                        <input type="text"  className="form-control"
                               placeholder="Doe"/>
                    </div>

                    <div className="form-group">
                        <label>PGY:</label>
                        <input type="text"  className="form-control"
                               placeholder="PGY-"/>
                    </div>

                    <div className="form-group">
                        <label>Date of Symptoms:</label>
                        <input type="date" className="form-control"/>
                    </div>

                    <div className="form-group">
                        <label>Symptoms Description:</label>
                        <textarea className="form-control" rows="3"></textarea>
                    </div>

                    <div className="form-group">
                        <label>COVID-19 Test Date:</label>
                        <input type="date" className="form-control"/>
                    </div>

                    {/*<div className="form-group">*/}
                    {/*    <label>COVID-19 Test Result:</label>*/}
                    {/*    */}
                    {/*</div>*/}

                    <div className="form-check">
                        <input className="form-check-input" type="radio" name="exampleRadios" id="exampleRadios1"
                               value="option1" checked/>
                            <label className="form-check-label">
                                Default radio
                            </label>
                    </div>
                    <div className="form-check">
                        <input className="form-check-input" type="radio" name="exampleRadios" id="exampleRadios2"
                               value="option2"/>
                            <label className="form-check-label" htmlFor="exampleRadios2">
                                Second default radio
                            </label>
                    </div>
                    <div className="form-check disabled">
                        <input className="form-check-input" type="radio" name="exampleRadios" id="exampleRadios3"
                               value="option3" disabled/>
                            <label className="form-check-label" htmlFor="exampleRadios3">
                                Disabled radio
                            </label>
                    </div>


                    {/*<div className="form-group">*/}
                    {/*    <label htmlFor="exampleFormControlSelect1">Example select</label>*/}
                    {/*    <select className="form-control" id="exampleFormControlSelect1">*/}
                    {/*        <option>1</option>*/}
                    {/*        <option>2</option>*/}
                    {/*        <option>3</option>*/}
                    {/*        <option>4</option>*/}
                    {/*        <option>5</option>*/}
                    {/*    </select>*/}
                    {/*</div>*/}


                </form>

            </div>
        );
    }
}
