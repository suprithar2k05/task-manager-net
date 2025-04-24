import { useState } from 'react'
import { Navigate, useNavigate } from 'react-router-dom'
import API from '../services/api'
import { toast } from 'react-toastify'
import { validateUsers } from '../utils/validate';
import { useUser } from '../context/UserContext';

const Login = () => {
  const [form, setForm] = useState({ email: '', password: '' });
  const [errors, setErrors] = useState({});
  const { user, setUser } = useUser();
  const navigate = useNavigate();

  if (user) {
    return <Navigate to="/" replace />;
  }

  const handleChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value })

  const handleSubmit = async e => {
    e.preventDefault();
    const [hasError, formErrors] = validateUsers(form, true);
    setErrors({...formErrors});
    if(hasError) return;
    try {
      const res = await API.post('/auth/login', form);
      if (res.data?.data?.userId) {
        console.log(res.data);
        toast.success("Login successful!");
        setUser(res.data?.data?.userId);
      } else {
        toast.error("Login failed!");
      }
    } catch (err) {
      console.error(err)
      toast.error(err.response?.data?.message || 'Login failed')
    }
  }

  return (
    <section className="max-w-md mx-auto mt-10 bg-white p-6 rounded shadow-md">
      <h2 className="text-2xl font-bold mb-4 text-center">Login</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
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
          className="w-full bg-blue-600 hover:bg-blue-700 cursor-pointer text-white font-semibold py-2 rounded"
        >
          Log In
        </button>
      </form>
    </section>
  )
}

export default Login
