import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import './css/EditEmployee.css';


const EditEmployeePage = () => {
    const [name, setName] = useState('');
    const [email, setEmailAddress] = useState('');
    const [phone, setPhoneNumber] = useState('');
    const [gender, setGender] = useState('');
    const [startDate, setStartDate] = useState('');
    const [cafeId, setCafeId] = useState('');
    const [id,setId]= useState('');
    const [cafes, setCafes] = useState([]);
    //const { cafeId } = useParams();
    let  Id  ;
    const [UserId, setUserId] =useState([]);
    const navigate = useNavigate();
    let  cafename  ;
    let updatedId;
    

    useEffect(() => {
        
        try{
            axios.get('http://localhost:5294/api/cafes/cafe/'+ "default").then(response => setCafes(response.data));
            console.log("cafes" + cafes);
            }
            catch (error) {
                console.error('Error adding cafe:', error);
            }
       
        
        cafename = window.location.href.split('edit-employee/');
        cafename = cafename[1];
        updatedId = cafename.split('_');
        Id= updatedId[0];
        console.log(Id);
        fetchEmployeeDetails(updatedId[1]);
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
          
        const newEmployee = {
            id,
            name,
            email,
            phone,
            gender,
            startDate,
            cafeId,
            
        };
        const random_uuid = uuidv4();
       

        try {
            await axios.put(`http://localhost:5294/api/cafes/employee/update`, newEmployee,{
                headers: {
                    'Content-Type': 'application/json',
                    'x-requestid':random_uuid
                },
            });
            navigate(`/employees`);
        } catch (error) {
            console.error('Error adding employee:', error);
        }
    };

    const fetchEmployeeDetails = async () => {
        try {
            debugger;
            const response = await axios.get('http://localhost:5294/api/cafes/employee/'+updatedId[1]);
            const cafeItem = response.data.find(item => item.id === updatedId[0]);
            //setEmployees(response.data);
            setName(cafeItem.name);
            setEmailAddress(cafeItem.email);
            setPhoneNumber(cafeItem.phone);
            setGender(cafeItem.gender);
            setId(cafeItem.id);
            const date = new Date(cafeItem.startDate);
            const day = String(date.getUTCDate()).padStart(2, '0'); 
           const month = String(date.getUTCMonth() + 1).padStart(2, '0'); 
           // Months are 0-based 
           const year = date.getUTCFullYear(); 
           // Combine the components into the desired format (dd-mm-yyyy) 
           const formattedDate = `${day}-${month}-${year}`;
            setStartDate(formattedDate);
            
        } catch (error) {
            console.error('Error fetching employees:', error);
        }
    };



    return (
        <div className="container">
            <h2>Edit New Employee</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name:</label>
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Email Address:</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmailAddress(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Phone Number:</label>
                    <input
                        type="text"
                        value={phone}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Gender:</label>
                    <select
                        value={gender}
                        onChange={(e) => setGender(e.target.value)}
                        required
                    >
                        <option value="">Select Gender</option>
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                        <option value="Other">Other</option>
                    </select>
                </div>
                <div>
                    <label>Start Date:</label>
                    <input
                        type="date"
                        value={startDate}
                        onChange={(e) => setStartDate(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                <label>Assigned Café</label>
                <select value={cafeId} onChange={(e) => setCafeId(e.target.value)}  required className="form-control">
                    <option value="">Select a café</option>
                    {cafes.map(cafe => (
                        <option key={cafe.id} value={cafe.id}>{cafe.name}</option>
                    ))}
                </select>
               
            </div>
                <button type="submit">Add Employee</button>
            </form>
        </div>
    );
};
function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
    .replace(/[xy]/g, function (c) {
        const r = Math.random() * 16 | 0, 
            v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

export default EditEmployeePage;
