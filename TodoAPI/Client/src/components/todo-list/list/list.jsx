import React, { useState } from "react";
import BasePopup from "../../popups/popup";
import TaskPopup from "../../popups/task-popup/task-popup";
import TodoList from "../todo-list";

export default function List({
  className,
  list,
  nameList,
  display,
  sendCurrentTaskForPopup,
}) {
  const currentList = list;
  const [isVisiable, setIsVisiable] = useState(display);
  const [popupStatus, setPopupStatus] = useState(false);

  function handleOpenPopup(task) {
    setPopupStatus(!popupStatus);
    sendCurrentTaskForPopup(popupStatus, task);
  }

  function handleChangeVisiable() {
    setIsVisiable(!isVisiable);
  }

  const displayByMaxLenght = (text, maxLength) =>
    text.length > maxLength ? text.substring(0, maxLength) : text;

  return (
    <div className={className}>
      <h3 onClick={handleChangeVisiable}>{nameList}</h3>
      <ul className={isVisiable ? "display-list" : "not-display-list"}>
        {currentList.map((task) => (
          <li
            key={task.id}
            className="more-about-current-task"
            onClick={() => handleOpenPopup(task)}
          >
            <h4>{displayByMaxLenght(task.title, 35)}</h4>
            <p>{displayByMaxLenght(task.content, 50)}</p>
          </li>
        ))}
      </ul>

      {/* <PopupContext.Provider
        value={{ isTriggered: popupStatus, task: selectedTask }}
      >
        <TodoList isTriggered={popupStatus} task={selectedTask} />
      </PopupContext.Provider> */}
      {/* {selectedTask && (
        <BasePopup trigger={popup}>
          <TaskPopup
            title={selectedTask.title}
            content={selectedTask.content}
            isComplete={selectedTask.isComplete}
          />
        </BasePopup>
      )} */}
    </div>
  );
}
