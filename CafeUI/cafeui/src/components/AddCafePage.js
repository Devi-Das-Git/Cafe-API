import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './css/AddCafe.css';
const AddCafePage = () => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [location, setLocation] = useState('');
    const [logo, setLogo] = useState(null);
    const navigate = useNavigate();

    const handleLogoChange = (e) => {
        setLogo(e.target.files[0]);
    };

    const handleCancel = () => { navigate('/'); 
        // Redirect to home or any other page 
        };

    const handleSubmit = async (e) => {
        e.preventDefault();
           // Generate a random UUID
const random_uuid = uuidv4();
        const formData = new FormData();
        formData.append('userId',random_uuid)
        formData.append('name', name);
        formData.append('description', description);
        formData.append('location', location);
        if (logo) formData.append('logo', logo);

     
        try {
            await axios.post('http://localhost:5294/api/cafes/cafe/create', formData, {
                headers: {
                    'Content-Type': 'application/json',
                    'x-requestid':random_uuid
                },
            });
            navigate('/');
        } catch (error) {
            console.error('Error adding cafe:', error);
        }
    };

    return (
        <div className="container">
            <h2>Add New Café</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name:</label>
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                        minLength={6} maxLength={10}
                    />
                </div>
                <div>
                    <label>Description:</label>
                    <textarea
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        required
                        minLength={6} maxLength={256}
                    ></textarea>
                </div>
                <div>
                    <label>Location:</label>
                    <input
                        type="text"
                        value={location}
                        onChange={(e) => setLocation(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Logo:</label>
                    <input
                        type="file"
                        accept="image/*"
                        onChange={handleLogoChange}
                    />
                </div>
                <button type="submit">Add Café</button>
                <button type="button" className="btn btn-secondary" onClick={handleCancel}>Cancel</button>
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
export default AddCafePage;
