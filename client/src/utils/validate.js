export const validateUsers = (form =  {}, isLogin = false) => {
   const newErrors = {};
   const {username, email, password} = form;
   // Username
   if (!username?.trim() && !isLogin) newErrors.username = "Username is required";

   // Email
   const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
   if (!email) newErrors.email = "Email is required";
   else if (!emailRegex.test(email))
     newErrors.email = "Invalid email format";

   // Password
   if (!password) newErrors.password = "Password is required";
   else if (password.length < 6 && !isLogin)
     newErrors.password = "Password must be at least 6 characters";

   return [Object.keys(newErrors).length > 0, newErrors];
 };

 export const validateTask = (form =  {}) => {
  const newErrors = {};
  const {title, dueDate} = form;

  if (!title?.trim()) newErrors.title = "title is required";

  if (!dueDate?.trim()) newErrors.dueDate = "Due date is required";

  return [Object.keys(newErrors).length > 0, newErrors];
};