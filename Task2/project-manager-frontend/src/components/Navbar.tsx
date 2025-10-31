import { Link, useNavigate } from "react-router-dom";
import "./Navbar.css";

export default function Navbar() {
  const navigate = useNavigate();

  const logout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <nav className="navbar">
      <Link to="/">Project Manager</Link>
      <button onClick={logout} className="logout-btn">
        Logout
      </button>
    </nav>
  );
}
