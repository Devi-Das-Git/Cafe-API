import React, { useState, useEffect } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './css/CafePage.css';



// const actionCellRenderer = (props) => { 
//      const handleDelete = () => { 
//         console.log(props.data)  
//         try {
//         axios.delete(`http://localhost:5294/api/cafes/cafe/`+props.data.id, {
//             headers: {
//                 'Content-Type': 'application/json',
//                 'x-requestid':props.data.id
//             },
//         });
//         const updatedRowData = props.rowData.filter(row => row.id !== props.data.id); api.setRowData(updatedRowData);
//     }  catch (error) {
//         console.error('Error deleting cafe:', error);
//     }};
    
    
//      return ( <div width="100px"> <button onClick={handleDelete}>Delete</button>   </div> ); 
// };

const CafePage = () => {
    const [cafes, setCafes] = useState([]);
    const [locationFilter, setLocationFilter] = useState('');
    const navigate = useNavigate();
    const [gridApi, setGridApi] = useState(null);
    
    useEffect(() => {
        fetchCafes();
    }, []);
    const onGridReady = (params) => { setGridApi(params.data); };
    const fetchCafes = async () => {
        try {
            if (locationFilter.trim() === '') { 
              
                const response = await axios.get('http://localhost:5294/api/cafes/cafe/'+ "default"); 
                setCafes(response.data);
            }
         else {  
            const response = await axios.get('http://localhost:5294/api/cafes/cafe/'+ locationFilter);
            setCafes(response.data);
         }
        } catch (error) {
            console.error('Error fetching cafes:', error);
        }
    };

    const actionCellRenderer = (props) => { 
        const handleDelete = () => { 
           console.log(props.data)  
           try {
           axios.delete(`http://localhost:5294/api/cafes/cafe/`+props.data.id, {
               headers: {
                   'Content-Type': 'application/json',
                   'x-requestid':props.data.id
               },
           });
        //    setCafes(props.rowData.filter(row => row.id !== props.data.id)); 
        //    gridApi.setCafes(props.rowData.filter(row => row.id !== props.data.id));
        //    gridApi.setCafes(cafes.filter(cafe => cafe.location.includes(locationFilter)));
        window.location.reload()
       }  catch (error) {
           console.error('Error deleting cafe:', error);
       }};
       
       
     return ( <div> <button onClick={handleDelete}>Del</button> 
        <button onClick={(id) => navigate(`/edit-cafe/${props.data.id}`)}>Edit</button>   
        </div> ); 
   };

    const columns = [
        {
            headerName: "action",
            minWidth: 160,
            cellRenderer: actionCellRenderer,
            editable: false,
            colId: "action"
          },
        //{ headerName: 'Logo', field: 'logo', cellRenderer: 'LogoCellRenderer' },
        { headerName: 'Name', field: 'name' },
        { headerName: 'Description', field: 'description' },
        { headerName: 'Employees', field: 'employees', cellRenderer: EmployeesCellRenderer },
        { headerName: 'Location', field: 'location' }
        
    ];

    const defaultColDef = {
        flex: 1,
        minWidth: 100,
        resizable: true,
    };

    return (
        <div className="ag-theme-alpine" style={{ height: '600px',width:'100%' }}>
            <h1>Cafe Home</h1>
            <div>
                <input
                    type="text"
                    placeholder="Filter by Location"
                    value={locationFilter}
                    onChange={(e) => setLocationFilter(e.target.value)}
                />
                <button onClick={() => fetchCafes()}>Filter</button>
                <button onClick={() => navigate('/add-cafe')}>Add New Café</button>
            </div>
            <AgGridReact
                rowData={cafes.filter(cafe => cafe.location.includes(locationFilter))}
                columnDefs={columns}
                defaultColDef={defaultColDef}
                frameworkComponents={{
                    LogoCellRenderer: LogoCellRenderer,
                    EmployeesCellRenderer: EmployeesCellRenderer,
                    ActionCellRenderer: actionCellRenderer,
                }}
                onGridReady={onGridReady}
                
            />
        </div>
    );
};

const LogoCellRenderer = (props) => {
    return <img src={props.value} alt="Logo" style={{ width: '50px', height: '50px' }} />;
};

const EmployeesCellRenderer = (props) => {
    const navigate = useNavigate();
     return <button  onClick={() => navigate(`/employees/${props.data.name}`)}>{props.data.employees}</button>;
};

  


export default CafePage;
