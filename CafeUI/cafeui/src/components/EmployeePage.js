import React, { useState, useEffect } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import axios from 'axios';
import {  useNavigate } from 'react-router-dom';
import './css/EmployeePage.css';

const actionCellRenderer = (props) => { 
    const handleEdit = () => {         
    //     const random_uuid = uuidv4();
        const formData = new FormData();
        formData.append('userId',props.data.id)
        formData.append('name', props.data.name);
        formData.append('description', props.data.description);
        formData.append('location', props.data.location);
        
     
        try {
            axios.update('http://localhost:5294/api/cafes/cafe/update', formData, {
                headers: {
                    'Content-Type': 'application/json',
                    'x-requestid':props.data.id
                },
            });
            
        } catch (error) {
            console.error('Error adding cafe:', error);
        }
    };

     const handleDelete = () => { 
        console.log(props.data)  
        try {
        axios.delete(`http://localhost:5294/api/cafes/employee/`+props.data.id, {
            headers: {
                'Content-Type': 'application/json',
                'x-requestid':props.data.id
            },
        });
        window.location.reload()
    }  catch (error) {
        console.error('Error deleting cafe:', error);
    }};
    
    
     return ( <div width="100px"> <button onClick={handleDelete}>Delete</button>   </div> ); 
};


const EmployeePage = () => {
    const [employees, setEmployees] = useState([]);
    // const { cafeId } = useParams();
    const navigate = useNavigate();
    const [cafeId, setLocationFilter] = useState('Cafe Delight');
    useEffect(() => {
        fetchEmployees();
    }, [cafeId]);

    const fetchEmployees = async () => {
        try {
            const response = await axios.get('http://localhost:5294/api/cafes/employee/'+cafeId);
            setEmployees(response.data);
        } catch (error) {
            console.error('Error fetching employees:', error);
        }
    };

    
    const columns = [
         
        {
            headerName: "action",
            minWidth: 150,
            cellRenderer: actionCellRenderer,
            editable: false,
            colId: "action"
          },
        { headerName: 'Name', field: 'name' },
        { headerName: 'Email', field: 'email' },
        { headerName: 'Phone', field: 'phone' },
        { headerName: 'Gender', field: 'gender' },
        { headerName: 'Start Date', field: 'startDate' },
        
    ];
    
    const defaultColDef = {
        flex: 1,
        minWidth: 100,
        resizable: true,
    };

    return (
        <div className="ag-theme-alpine" style={{ height: '600px', width: '100%' }}>
            <h1>Employee Home</h1>
            <input
                    type="text"
                    placeholder="Filter by Location"
                    value={cafeId}
                    onChange={(e) => setLocationFilter(e.target.value)}
                />
                <button onClick={() => fetchEmployees()}>Filter</button>
            <button onClick={() => navigate('/add-employee')}>Add New Employee</button>
            <AgGridReact
                rowData={employees}
                columnDefs={columns}
                defaultColDef={defaultColDef}
                frameworkComponents={{
                    dateRenderer: DateRenderer,
                    ActionCellRenderer: actionCellRenderer,
                }}
            />
        </div>
    );
};

const DateRenderer = (props) => {
    return new Date(props.value).toLocaleDateString();
};

// const ActionCellRenderer = (props) => {
//     return (
//         <>
//             <button onClick={() => props.onEdit(props.data.id)}>Edit</button>
//             <button onClick={() => props.onDelete(props.data.id)}>Delete</button>
//         </>
//     );
// };

export default EmployeePage;
