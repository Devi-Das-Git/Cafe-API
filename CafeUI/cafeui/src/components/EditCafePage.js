import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';

const EditCafePage = () => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [location, setLocation] = useState('');
    const [logo, setLogo] = useState(null);
    const { id } = useParams();
    const [UserId, setUserId] =useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        fetchCafeDetails();
    }, [id]);

    const handleCancel = () => { navigate('/'); 
        // Redirect to home or any other page 
        };
   const fetchCafeDetails = async () => {
        try {
            const response = await axios.get('http://localhost:5294/api/cafes/cafe/'+ "default"); 
            const cafe = response.data;
            const cafeItem = cafe.find(item => item.id === id);
            setName(cafeItem.name);
            setDescription(cafeItem.description);
            setLocation(cafeItem.location);
            setUserId(cafeItem.id)
            console.log(name+"-"+UserId);
            // Handle the logo if needed
        } catch (error) {
            console.error('Error fetching cafe details:', error);
        }
    };

    const handleLogoChange = (e) => {
        setLogo(e.target.files[0]);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append('name', name);
        formData.append('description', description);
        formData.append('location', location);
        formData.append('UserId',UserId)
        if (logo) formData.append('logo', logo);

        try {
            await axios.put('http://localhost:5294/api/cafes/cafe/update', formData, {
                headers: {
                    'Content-Type': 'application/json',
                    'x-requestid':UserId
                },
            });
            navigate('/');
        } catch (error) {
            console.error('Error adding cafe:', error);
        }
    };

    return (
        <div className="container">
            <h2>Edit Café</h2>
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
                <button type="submit">Update Café</button>
                <button type="button" className="btn btn-secondary" onClick={handleCancel}>Cancel</button>
            </form>
        </div>
    );
};

export default EditCafePage;
