import React, { useState,useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import './css/AddEmployee.css';
const AddEmployeePage = () => {
    const [name, setName] = useState('');
    const [email, setEmailAddress] = useState('');
    const [phone, setPhoneNumber] = useState('');
    const [gender, setGender] = useState('');
    const [startDate, setStartDate] = useState('');
    //const { cafeId } = useParams();
    const [cafeId, setCafeId] = useState('');
    const [cafes, setCafes] = useState([]);
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();

    const validate = () => {
        const errors = {};
        if (name.length < 6 || name.length > 10) errors.name = 'Name should be between 6 and 10 characters.';
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(email)) errors.email = 'Invalid email address.';
        const phonePattern = /^[89]\d{7}$/;
        if (!phonePattern.test(phone)) errors.phone = 'Invalid Singapore phone number.';
        if (!gender) errors.gender = 'Gender is required.';
        if (!cafeId) errors.cafeId = 'Assigned Café is required.';
        return errors;
    };

    useEffect(() => {
        // Fetch cafes for dropdown
        axios.get('http://localhost:5294/api/cafes/cafe/'+ "default").then(response => setCafes(response.data));

   
    }, []);


    const handleSubmit = async (e) => {
        e.preventDefault();
          
        const newEmployee = {
            name,
            email,
            phone,
            gender,
            startDate,
            cafeId,
            
        };
        const random_uuid = uuidv4();
        const formData = new FormData();
        formData.append('id',random_uuid)
        formData.append('name', name);
        formData.append('email', email);
        formData.append('phone', phone);
        formData.append('cafeId', cafeId);
        formData.append('gender', gender);
        //if (logo) formData.append('logo', logo);

        try {
            await axios.post(`http://localhost:5294/api/cafes/employee/create`, newEmployee,{
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

    return (
        <div className="container">
            <h2>Add New Employee</h2>
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
                <select value={cafeId} onChange={(e) => setCafeId(e.target.value)} className="form-control">
                    <option value="">Select a café</option>
                    {cafes.map(cafe => (
                        <option key={cafe.id} value={cafe.id}>{cafe.name}</option>
                    ))}
                </select>
                {errors.cafeId && <div className="text-danger">{errors.cafeId}</div>}
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
export default AddEmployeePage;
