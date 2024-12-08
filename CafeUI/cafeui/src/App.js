import React from 'react';
// import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import CafePage from './components/CafePage';
import EmployeePage from './components/EmployeePage';
import AddCafePage from './components/AddCafePage';
import EditCafePage from './components/EditCafePage';  // Import the EditCafePage component
import AddEmployeePage from './components/AddEmployeePage';
import EditEmployeePage from './components/EditEmployeePage';
import './App.css';
const App = () => {
    return (
        <Router>
            <header>
                <h1>Cafe Master</h1>
            </header>
            <nav> <ul> 
                <li><h1><Link to="/">Cafe</Link></h1></li> 
                <li><h1><Link to="/employees">Employee</Link></h1></li> 
                </ul>
            </nav>
            <div className="container">
            <Routes> 
            <Route path="/" element={<CafePage />} />
            <Route path="/employees/:id?" element={<EmployeePage />} />
                 <Route path="/add-cafe" element={<AddCafePage />} />
                    <Route path="/edit-cafe/:id" element={<EditCafePage />} />
                    <Route path="/add-employee" element={<AddEmployeePage />} />
                    <Route path="/edit-employee/:id" element={<EditEmployeePage />} /> 
            </Routes>
            </div>
            {/* <div className="container">
                <Routes>
                    <Route path="/" element={<CafePage />} />
                    <Route path="/employees/:cafeId" element={<EmployeePage />} />
                    <Route path="/add-cafe" element={<AddCafePage />} />
                    <Route path="/edit-cafe/:id" element={<EditCafePage />} />
                    <Route path="/add-employee" element={<AddEmployeePage />} />
                    <Route path="/edit-employee/:id" element={<EditEmployeePage />} />
                </Routes>
            </div> */}
        </Router>
    );
};

export default App;
