import React, { Component } from "react";
import { Col, Container, Row } from "reactstrap";

class Presentation extends Component {
  constructor(props) {
    super(props);

    this._handleOnClick = this._handleOnClick.bind(this);
  }

  _handleOnClick() {
    const { history } = this.props;
    history.push("/Orders/orders");
  }

  render() {
    return (
      <Container>
        <Row className="justify-content-center">
          <Col md="6">
            <div
              style={{ cursor: "pointer" }}
              className="clearfix"
              onClick={() => {
                this._handleOnClick();
              }}
            >
              <h1 className="float-left display-3 mr-4">GESTAMP</h1>
              <h4 className="pt-3">Test</h4>
              <p className="text-muted float-left">CRUD basado en Orders</p>
            </div>
          </Col>
        </Row>
      </Container>
    );
  }
}

export default Presentation;
