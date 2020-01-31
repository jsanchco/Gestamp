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
import { config, ORDERS } from "../../constants";
import { L10n } from "@syncfusion/ej2-base";
import data from "../../locales/locale.json";
import { connect } from "react-redux";
import ACTION_APPLICATION from "../../actions/applicationAction";
import { TOKEN_KEY } from "../../services";

L10n.load(data);

class Orders extends Component {
  orders = new DataManager({
    adaptor: new WebApiAdaptor(),
    url: `${config.URL_API}/${ORDERS}`,
    headers: [{ Authorization: "Bearer " + localStorage.getItem(TOKEN_KEY) }]
  });

  constructor(props) {
    super(props);

    this.state = {
      orders: null
    };

    this.toolbarOptions = ["Add", "Edit", "Delete", "Update", "Cancel"];
    this.editSettings = {
      showDeleteConfirmDialog: true,
      allowEditing: true,
      allowAdding: true,
      allowDeleting: true,
      newRowPosition: "Top"
    };
    this.pageSettings = { pageCount: 10, pageSize: 10 };
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
              <i className="icon-list"></i> Orders
            </div>
            <div className="card-body"></div>
            <Row>
              <GridComponent
                dataSource={this.orders}
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
                    field="Id"
                    headerText="Id"
                    width="100"
                    isPrimaryKey={true}
                    isIdentity={true}
                  />
                  <ColumnDirective
                    field="Region"
                    headerText="Region"
                    width="100"
                  />
                  <ColumnDirective
                    field="Country"
                    headerText="Country"
                    width="100"
                  />
                  <ColumnDirective
                    field="ItemType"
                    headerText="Item Type"
                    width="100"
                  />
                  <ColumnDirective
                    field="UnitPrice"
                    headerText="Unit Price"
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

Orders.propTypes = {};

const mapStateToProps = state => {
  return {
    errorApplication: state.applicationReducer.error
  };
};

const mapDispatchToProps = dispatch => ({
  showMessage: message => dispatch(ACTION_APPLICATION.showMessage(message))
});

export default connect(mapStateToProps, mapDispatchToProps)(Orders);
