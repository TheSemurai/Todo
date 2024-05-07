import { Route, Router, Routes } from "react-router-dom";
import TodoList from "./components/todo-list/todo-list";
import LoginPage from "./auth-pages/login/login-page";
import NoMatchRoutes from "./routes-pages/no-match-routes";
import RouteGuard from "./routes-pages/route-guards";
import SingUpPage from "./auth-pages/singup/singup-page";

function App() {
  return (
    <>
      <Routes history={history}>
        <Route element={<RouteGuard />}>
          <Route path="/" element={<TodoList />} exact />
        </Route>

        <Route path="*" element={<NoMatchRoutes />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/singup" element={<SingUpPage />} />
      </Routes>
    </>
  );
}

export default App;
