import React, { Component } from "react";
import PropTypes from "prop-types";

const propTypes = {
  children: PropTypes.node
};

const defaultProps = {};

class DefaultFooter extends Component {
  render() {
    // eslint-disable-next-line
    const { children, ...attributes } = this.props;

    return (
      <React.Fragment>
        <span>
          <a href="https://www.gestamp.com/es/home">Gestamp</a>
        </span>
        <span className="ml-auto">
          Creado por{" "}
          <a href="https://www.linkedin.com/in/jes%C3%BAs-s%C3%A1nchez-corzo-9b33b7167">
            Jesús Sánchez
          </a>
        </span>
      </React.Fragment>
    );
  }
}

DefaultFooter.propTypes = propTypes;
DefaultFooter.defaultProps = defaultProps;

export default DefaultFooter;
