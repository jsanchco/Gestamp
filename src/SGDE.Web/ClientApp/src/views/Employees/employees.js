import React, { Component, Fragment } from "react";
import { Row } from "reactstrap";
import {
  ColumnDirective,
  ColumnsDirective,
  GridComponent,
  Edit,
  Inject,
  Toolbar,
  Page,
  ForeignKey,
  Group
} from "@syncfusion/ej2-react-grids";
import { DataManager, WebApiAdaptor } from "@syncfusion/ej2-data";
import { config, USERS, PROFESSIONS } from "../../constants";
import { L10n } from "@syncfusion/ej2-base";
import data from "../../locales/locale.json";
import { connect } from "react-redux";
import ACTION_APPLICATION from "../../actions/applicationAction";
import { TOKEN_KEY } from "../../services";

L10n.load(data);

class Employees extends Component {
  users = new DataManager({
    adaptor: new WebApiAdaptor(),
    url: `${config.URL_API}/${USERS}`,
    headers: [{ Authorization: 'Bearer ' + localStorage.getItem(TOKEN_KEY) }]
  });

  professions = new DataManager({
    adaptor: new WebApiAdaptor(),
    url: `${config.URL_API}/${PROFESSIONS}`,
    headers: [{ Authorization: 'Bearer ' + localStorage.getItem(TOKEN_KEY) }]
  });

  proffesionIdRules = { required: true };

  constructor(props) {
    super(props);

    this.state = {
      users: null,
      professions: null
    };

    this.toolbarOptions = ["Add", "Edit", "Delete", "Update", "Cancel", "Search"];
    this.editSettings = {
      showDeleteConfirmDialog: true,
      allowEditing: true,
      allowAdding: true,
      allowDeleting: true,
      newRowPosition: "Top"
    };
    this.pageSettings = { pageCount: 2, pageSize: 2 };
    this.actionFailure = this.actionFailure.bind(this);
    this.actionComplete = this.actionComplete.bind(this);
  }

  actionFailure(args) {
    const error = Array.isArray(args.error) ? args.error[0] : args.error;
    this.props.showMessage({
      statusText: error.error.statusText,
      responseText: error.error.responseText,
      type: "danger"
    });
  }

  actionComplete(args) {
    if (args.requestType === "save") {
      this.props.showMessage({
        statusText: "200",
        responseText: "Operación realizada con éxito",
        type: "success"
      });
    }
    if (args.requestType === "delete") {
      this.props.showMessage({
        statusText: "200",
        responseText: "Operación realizada con éxito",
        type: "success"
      });
    }
  }

  render() {
    return (
      <Fragment>
        <div className="animated fadeIn">
          <div className="card">
            <div className="card-header">
              <i className="icon-list"></i> Trabajadores
            </div>
            <div className="card-body"></div>
            <Row>
              <GridComponent
                dataSource={this.users}
                locale="es-US"
                allowPaging={true}
                pageSettings={this.pageSettings}
                toolbar={this.toolbarOptions}
                editSettings={this.editSettings}
                style={{
                  marginLeft: 30,
                  marginRight: 30,
                  marginTop: -20,
                  marginBottom: 20
                }}
                actionFailure={this.actionFailure}
                actionComplete={this.actionComplete}
                allowGrouping={true}
              >
                <ColumnsDirective>
                  <ColumnDirective
                    field="id"
                    headerText="Id"
                    width="100"
                    isPrimaryKey={true}
                    isIdentity={true}
                  />
                  <ColumnDirective
                    field="name"
                    headerText="Nombre"
                    width="100"
                  />
                  <ColumnDirective
                    field="surname"
                    headerText="Apellidos"
                    width="100"
                  />
                  <ColumnDirective
                    field="professionId"
                    headerText="Profesión"
                    width="100"
                    editType="dropdownedit"
                    foreignKeyValue="name"
                    foreignKeyField="id"
                    validationRules={this.proffesionIdRules}
                    dataSource={this.professions}
                  />
                  <ColumnDirective
                    field="age"
                    headerText="Edad"
                    width="100"
                    textAlign="Right"
                  />
                </ColumnsDirective>
                <Inject services={[ForeignKey, Group, Page, Toolbar, Edit]} />
              </GridComponent>
            </Row>
          </div>
        </div>
      </Fragment>
    );
  }
}

Employees.propTypes = {};

const mapStateToProps = state => {
  return {
    errorApplication: state.applicationReducer.error
  };
};

const mapDispatchToProps = dispatch => ({
  showMessage: message => dispatch(ACTION_APPLICATION.showMessage(message))
});

export default connect(mapStateToProps, mapDispatchToProps)(Employees);
