import React, { Component, Suspense, Fragment } from "react";
import { Redirect, Route, Switch } from "react-router-dom";
import * as router from "react-router-dom";
import { Container } from "reactstrap";
import { connect } from "react-redux";

import {
  AppAside,
  AppFooter,
  AppHeader,
  AppSidebar,
  AppSidebarFooter,
  AppSidebarForm,
  AppSidebarHeader,
  AppSidebarMinimizer,
  AppBreadcrumb2 as AppBreadcrumb,
  AppSidebarNav2 as AppSidebarNav
} from "@coreui/react";
// sidebar nav config
import navigation from "../../_nav";
// routes config
import routes from "../../routes";
import ReactNotification, { store } from "react-notifications-component";
import "react-notifications-component/dist/theme.css";

const DefaultAside = React.lazy(() => import("./DefaultAside"));
const DefaultFooter = React.lazy(() => import("./DefaultFooter"));
const DefaultHeader = React.lazy(() => import("./DefaultHeader"));

class DefaultLayout extends Component {
  componentDidUpdate(prevProps) {
    if (
      this.props.messageApplication != null &&
      prevProps.messageApplication !== this.props.messageApplication
    ) {
      this.showMessage();
    }
  }

  loading = () => (
    <div className="animated fadeIn pt-1 text-center">Cargando...</div>
  );

  signOut(e) {
    e.preventDefault();
    this.props.history.push("/login");
  }

  showMessage() {
    let message;
    if (this.props.messageApplication.type === "danger") {
      message = this.props.messageApplication.statusText;
      console.log("error ->", this.props.messageApplication.responseText);
    } else {
      message = this.props.messageApplication.responseText;
    }
    if (message === "") {
      message = "ERROR en la petición";
    }

    store.addNotification({
      message: message,
      type: this.props.messageApplication.type,
      container: "bottom-center",
      animationIn: ["animated", "fadeIn"],
      animationOut: ["animated", "fadeOut"],
      dismiss: {
        duration: 5000,
        showIcon: true
      },
      width: 800
    });
  }
  
  render() {
    return (
      <Fragment>
        <ReactNotification />
        <div className="app">
          <AppHeader fixed>
            <Suspense fallback={this.loading()}>
              <DefaultHeader onLogout={e => this.signOut(e)} />
            </Suspense>
          </AppHeader>
          <div className="app-body">
            <AppSidebar fixed display="lg">
              <AppSidebarHeader />
              <AppSidebarForm />
              <Suspense>
                <AppSidebarNav
                  navConfig={navigation}
                  {...this.props}
                  router={router}
                />
              </Suspense>
              <AppSidebarFooter />
              <AppSidebarMinimizer />
            </AppSidebar>
            <main className="main">
              <AppBreadcrumb appRoutes={routes} router={router} />
              <Container fluid>
                <Suspense fallback={this.loading()}>
                  <Switch>
                    {routes.map((route, idx) => {
                      return route.component ? (
                        <Route
                          key={idx}
                          path={route.path}
                          exact={route.exact}
                          name={route.name}
                          render={props => <route.component {...props} />}
                        />
                      ) : null;
                    })}
                    <Redirect from="/" to="/dashboard" />
                  </Switch>
                </Suspense>
              </Container>
            </main>
            <AppAside fixed>
              <Suspense fallback={this.loading()}>
                <DefaultAside />
              </Suspense>
            </AppAside>
          </div>
          <AppFooter>
            <Suspense fallback={this.loading()}>
              <DefaultFooter />
            </Suspense>
          </AppFooter>
        </div>
      </Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    messageApplication: state.applicationReducer.message
  };
};

export default connect(mapStateToProps)(DefaultLayout);
