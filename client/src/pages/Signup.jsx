import { useState } from 'react'
import { useNavigate, Navigate } from 'react-router-dom'
import API from '../services/api'
import { toast } from 'react-toastify'
import { validateUsers } from '../utils/validate'

const Signup = ({ user }) => {
  const [form, setForm] = useState({ username: '', email: '', password: '' });
  const [errors, setErrors] = useState({});
  
  if (user) {
    return <Navigate to="/" replace />;
  }
  const navigate = useNavigate()

  const handleChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value })

  const handleSubmit = async e => {
    e.preventDefault();
    const [hasError, formErrors] = validateUsers(form);
    setErrors(formErrors);
    if(hasError) return;
    try {
      await API.post('/auth/signup', form)
      toast.success('Signup successful!')
      navigate('/');
      window.location.href = '/';
    } catch (err) {
      console.error(err)
      toast.error(err.response?.data?.message || 'Signup failed')
    }
  }

  return (
    <section className="max-w-md mx-auto mt-10 bg-white p-6 rounded shadow-md">
      <h2 className="text-2xl font-bold mb-4 text-center">Sign Up</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
        <input
          type="text"
          name="username"
          placeholder="Name"
          className="w-full border border-gray-300 rounded px-3 py-2"
          value={form.name}
          onChange={handleChange}
        />
        {errors.username && (
          <p className="text-red-500 text-sm mt-1">{errors.username}</p>
        )}
        <input
          name="email"
          placeholder="Email"
          className="w-full border border-gray-300 rounded px-3 py-2"
          value={form.email}
          onChange={handleChange}
        />
        {errors.email && (
          <p className="text-red-500 text-sm mt-1">{errors.email}</p>
        )}
        <input
          type="password"
          name="password"
          placeholder="Password"
          className="w-full border border-gray-300 rounded px-3 py-2"
          value={form.password}
          onChange={handleChange}
        />
        {errors.password && (
          <p className="text-red-500 text-sm mt-1">{errors.password}</p>
        )}
        <button
          type="submit"
          className="w-full bg-green-600 hover:bg-green-700 cursor-pointer text-white font-semibold py-2 rounded"
        >
          Sign Up
        </button>
      </form>
    </section>
  )
}

export default Signup
