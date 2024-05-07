import { useNavigate } from "react-router-dom";

export default function NoMatchRoutes() {
  const navigate = useNavigate();

  return (
    <div className="wrong-route">
      <h2>Wrong routes</h2>
      <button className="btn btn-grey" onClick={() => navigate(-1)}>
        Go Back
      </button>
    </div>
  );
}
