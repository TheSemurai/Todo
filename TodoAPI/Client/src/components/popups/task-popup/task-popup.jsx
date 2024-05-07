import React, { useState } from "react";
import PropTypes from "prop-types";
import axios from "axios";
import TodoService from "../../../services/todo.service";

export default function TaskPopup(props) {
  const todoService = new TodoService();
  const [id, setId] = useState(props.id || "");
  const [title, setTitle] = useState(props.title || "");
  const [content, setContent] = useState(props.content || "");
  const [isComplete, setIsComplete] = useState(props.isComplete);

  function handleContentChange(event) {
    setContent(event.target.value);
  }

  function handleTitleChange(event) {
    setTitle(event.target.value);
  }

  function saveChanges() {
    const request = todoService.update(id, {
      title: title,
      content: content,
      isComplete: isComplete,
    });

    request
      .then(() => {
        props.refreshMyTask();
        props.setTrigger(false);
      })
      .catch((error) => {
        console.warn(
          `Sorry, something went wrong while updating task (title: ${title})`
        );
      });
  }

  function deleteTask() {
    const request = todoService.delete(id);

    request
      .then((response) => {
        props.refreshMyTask();
        props.setTrigger(false);
      })
      .catch((error) => {
        console.warn(
          `Sorry, something went wrong while deleting task (title: ${title})`
        );
      });
  }

  function handleChangeCompleteStatus(event) {
    setIsComplete(!isComplete);
  }

  return (
    <div className="popup-current-task">
      <div className="popup-input">
        <p>title:</p>
        <input
          className="inpt-text"
          type="text"
          value={title}
          onChange={handleTitleChange}
          placeholder="enter a new title.."
        />
        <p>content:</p>
        <input
          className="inpt-text"
          type="text"
          value={content}
          onChange={handleContentChange}
          placeholder="enter a new content.."
        />
      </div>
      <div className="popup-checkbox">
        <p>task status:</p>
        <input
          className="inpt-checkbox"
          type="checkbox"
          checked={isComplete}
          onChange={handleChangeCompleteStatus}
        />
      </div>
      <div className="popup-buttons">
        <button className="btn btn-blue" onClick={saveChanges}>
          Save
        </button>
        <button className="btn btn-red ml-20px" onClick={deleteTask}>
          Delete
        </button>
      </div>
    </div>
  );
}

TaskPopup.propTypes = {
  id: PropTypes.number,
  title: PropTypes.string,
  content: PropTypes.string,
  isComplete: PropTypes.bool,
};
