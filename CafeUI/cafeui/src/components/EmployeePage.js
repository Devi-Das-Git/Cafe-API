import React, { useState, useEffect } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import axios from 'axios';
import {  useNavigate,useParams } from 'react-router-dom';
import './css/EmployeePage.css';



const EmployeePage = () => {
    const [employees, setEmployees] = useState([]);
    // const { cafeId } = useParams();
    const navigate = useNavigate();
    const [cafeId, setLocationFilter] = useState('');
    let { id } = useParams();
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
            window.location.reload()
        }};
        
        
         return ( <div> 
            <button onClick={handleDelete}>Del</button> 
             <button onClick={(id,location) => navigate(`/edit-employee/${props.data.id + '_'+cafeId}`)}>Edit</button>  
               </div> ); 
    };
    
    useEffect(() => {
        if(id==undefined && cafeId ==''){
            setLocationFilter('Cafe Delight')
        }
        else if(id !='' && cafeId==''){
            setLocationFilter(id)
        }
        else{
            setLocationFilter(cafeId)
            id = cafeId
        }
        fetchEmployees();
    }, [cafeId]);

    const fetchEmployees = async () => {
        debugger
        try {
            if(id!=undefined){
            const response = await axios.get('http://localhost:5294/api/cafes/employee/'+id);
            setEmployees(response.data);
            }
            else{
                const response = await axios.get('http://localhost:5294/api/cafes/employee/'+cafeId);
                setEmployees(response.data);
                }
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
        { headerName: 'Days Worked', field: 'daysWorked' },
        { headerName: 'Cafe Name', field: 'cafe' },
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
                    placeholder="Filter by Cafe Name"
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


export default EmployeePage;
