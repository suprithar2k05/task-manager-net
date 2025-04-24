import { useState, useEffect } from 'react'
import API from '../services/api'
import { toast } from 'react-toastify'

const useTasks = () => {
  const [tasks, setTasks] = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)
  const [filters, setFilters] = useState({
   status: 'all', // 'all' | 'completed' | 'pending'
   tag: 'all',
   priority: 'all',
  })
  
 
  const filteredTasks = tasks.filter(task => {   return (
     (filters.status === 'all' || task.isCompleted === (filters.status === 'completed')) &&
     (filters.tag === 'all' || task.tags.includes(filters.tag)) &&
     (filters.priority === 'all' || task.priority === filters.priority)
   )
 })

  const fetchTasks = async () => {
    setLoading(true)
    try {
      const res = await API.get('/task');
      if(Array.isArray(res.data?.data)){
        if (res.data?.data.length == 0) {
          toast.error('No task found create one!', { toastId: 'show-error' });
        } 
        setTasks(res.data?.data)
      } 
      
    } catch (err) {
      setError(err);
      toast.error('Failed to load tasks', { toastId: 'catch-error'});
    } finally {
      setLoading(false)
    }
  }

  const createTask = async task => {
    try {
      console.log('tasks new >>>', task);
      const res = await API.post('/task', task);
      setTasks(prev => [res.data?.data, ...prev])
      toast.success('Task created!')
    } catch (err) {
      toast.error('Failed to create task');
      console.log('errr >>>', err.message);
    }
  }

  const updateTask = async (id, updatedData) => {
    try {
      const res = await API.put(`/task/${id}`, updatedData);

      setTasks(prev =>
        prev.map(t => (t.id === id ? res.data?.data : t))
      )
      toast.success('Task updated!')
    } catch (err) {
      toast.error('Failed to update task');
    }
  }

  const deleteTask = async id => {
    try {
      window.location.href = '/';
      await API.delete(`/task/${id}`)
      setTasks(prev => prev.filter(t => t.id !== id))
      toast.success('Task deleted!')
    } catch (err) {
      toast.error('Failed to delete task')
    }
  }

  const toggleComplete = async id => {
    const task = tasks.find(t => t.id === id)
    if (!task) return
    window.location.href = '/';
    updateTask(id, { ...task, isCompleted: !task.isCompleted })
  }

  useEffect(() => {
    fetchTasks()
  }, [])

  return {
    tasks,
    loading,
    error,
    createTask,
    updateTask,
    deleteTask,
    toggleComplete,
    filters, 
    setFilters, 
    filteredTasks
  }
}

export default useTasks
