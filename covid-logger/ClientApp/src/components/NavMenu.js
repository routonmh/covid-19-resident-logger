import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import {Glyphicon, Nav, Navbar, NavItem} from 'react-bootstrap';
import {LinkContainer} from 'react-router-bootstrap';
import './NavMenu.css';

export class NavMenu extends Component {
    displayName = NavMenu.name

    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/'}>COVID-19 Resident Log</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle/>
                </Navbar.Header>
                <Navbar.Collapse>
                    <Nav>
                        <LinkContainer to={'/'} exact>
                            <NavItem>
                                <Glyphicon glyph='home'/> Data Entry
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/residents'}>
                            <NavItem>
                                <Glyphicon glyph='user'/> Residents
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/assignments'}>
                            <NavItem>
                                <Glyphicon glyph='th-list'/> Assignments
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/available'}>
                            <NavItem>
                                <Glyphicon glyph='ok'/> Available
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/on-duty'}>
                            <NavItem>
                                <Glyphicon glyph='time'/> On Duty
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/quarantined'}>
                            <NavItem>
                                <Glyphicon glyph='ban-circle'/> Quarantined
                            </NavItem>
                        </LinkContainer>
                        <LinkContainer to={'/tested-positive'}>
                            <NavItem>
                                <Glyphicon glyph='warning-sign'/> Tested Postive
                            </NavItem>
                        </LinkContainer>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}
