import { useRef, useEffect, useState } from "react";
import List from "./list/list";
import NavTodoBar from "../nav-bar/nav-todo-bar";
import BasePopup from "../popups/popup";
import TaskPopup from "../popups/task-popup/task-popup";
import axios from "axios";
import TodoService from "../../services/todo.service";

export default function TodoList() {
  const todoService = new TodoService();
  const [isTriggered, setIsTriggered] = useState(false);
  const [popupCurrentTask, setPopupCurrentTask] = useState({
    id: 0,
    title: "",
    content: "",
    isComplete: false,
  });
  const [newTask, setNewTask] = useState({
    title: "",
    content: "",
  });
  const [list, setList] = useState([]);

  const todoShould = separateList(false);
  const todoDone = separateList(true);

  function separateList(doneStatus) {
    return list.length > 0
      ? list.filter((element) => element.isComplete === doneStatus)
      : [];
  }

  const receiveCurrentTaskForPopupFromList = (popupStatus, task) => {
    setIsTriggered(popupStatus);
    setPopupCurrentTask(task);
  };

  useEffect(() => {
    refreshList();
  }, []);

  function refreshList() {
    const request = todoService.getAllTasks();

    request
      .then((response) => {
        setList(response.data);
      })
      .catch((error) => {
        if (axios.isCancel(error)) {
          console.warn("Request canceled:", error.message);
        } else {
          console.warn("Another kind of issue:", error.message);
        }
      });
  }

  function handleCreateTask() {
    const request = todoService.create(newTask);

    request
      .then(() => {
        setNewTask((prevTask) => ({ ...prevTask, title: "" }));
        refreshList();
      })
      .catch((error) => console.error(error));
  }

  function handleTitleChange(event) {
    setNewTask((task) => ({ ...task, title: event.target.value }));
  }

  return (
    <>
      <NavTodoBar />
      <div className="todo-container">
        <div className="todo-article">
          <div className="new-task-creation">
            <input
              className="inpt-text"
              type="text"
              value={newTask.title}
              onChange={handleTitleChange}
              placeholder="enter your task.."
              maxLength={65}
            />
            <button
              className="btn btn-green ml-20px"
              onClick={handleCreateTask}
            >
              Create
            </button>
          </div>
        </div>
        <div className="todo-content">
          <List
            className="todo todo-should"
            nameList={"todo:"}
            list={todoShould}
            display={true}
            sendCurrentTaskForPopup={receiveCurrentTaskForPopupFromList}
          />
          <List
            className="todo todo-done"
            nameList={"done:"}
            list={todoDone}
            display={false}
            sendCurrentTaskForPopup={receiveCurrentTaskForPopupFromList}
          />
        </div>
      </div>

      {/* <p>{isTriggered ? "yes" : "no"}</p> */}

      {popupCurrentTask && (
        <BasePopup trigger={isTriggered} setTrigger={setIsTriggered}>
          <TaskPopup
            className="current-task-popup"
            refreshMyTask={refreshList}
            setTrigger={setIsTriggered}
            id={popupCurrentTask.id}
            title={popupCurrentTask.title}
            content={popupCurrentTask.content}
            isComplete={popupCurrentTask.isComplete}
          />
        </BasePopup>
      )}
    </>
  );
}
