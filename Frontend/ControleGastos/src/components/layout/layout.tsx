import { Outlet } from 'react-router-dom';
import Sidebar from './sidebar';

function Layout() {
  return (
    <>
      <nav className="navbar navbar-dark bg-primary fixed-top">
        <div className="container-fluid">
          <button
            className="navbar-toggler d-lg-none"
            type="button"
            data-bs-toggle="offcanvas"
            data-bs-target="#sidebar"
          >
            <span className="navbar-toggler-icon"></span>
          </button>

          <span className="navbar-brand fw-bold">
            💰 Sistema de controle de gastos
          </span>
        </div>
      </nav>

      <div className="d-none d-lg-block position-fixed top-0 start-0 vh-100 bg-dark pt-5" style={{ width: 250 }}>
        <Sidebar />
      </div>

      <main className="pt-5"
          style={{ marginLeft: 250 }}>
        <div className="container-fluid p-4">
          <Outlet />
        </div>
      </main>
    </>
  );
}

export default Layout;