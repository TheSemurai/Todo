import { useNavigate } from "react-router-dom";
import AuthService from "../../services/auth.service";

export default function NavTodoBar() {
  const authService = new AuthService();
  const navigate = useNavigate();

  const logout = () => {
    authService.logout();
    navigate("/login");
  };

  const renderLogout = () => {
    return authService.isUserLoggined() ? null : (
      <label onClick={logout}>logout</label>
    );
  };

  return (
    <nav className="todo-nav">
      <p>Todoshka</p>
      {renderLogout()}
    </nav>
  );
}
