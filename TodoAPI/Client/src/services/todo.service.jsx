import React from "react";
import HttpService from "./http.service";

class TodoService extends React.Component {
  constructor(
    mainUrl = "/Task",
    getAllTasksUrl = "/GetAllTasks",
    createTaskUrl = "/CreateTask",
    deleteTaskUrl = "/DeleteTask",
    updateTaskUrl = "/UpdateTask"
  ) {
    super();
    this.mainUrl = mainUrl;
    this.getAllTasksUrl = getAllTasksUrl;
    this.createTaskUrl = createTaskUrl;
    this.deleteTaskUrl = deleteTaskUrl;
    this.updateTaskUrl = updateTaskUrl;
    this.httpService = new HttpService();
  }

  getAllTasks() {
    return this.httpService.get(this.mainUrl + this.getAllTasksUrl);
  }

  create(data) {
    return this.httpService.post(this.mainUrl + this.createTaskUrl, data);
  }

  update(id, data) {
    return this.httpService.patch(
      this.mainUrl + this.updateTaskUrl + `/${id}`,
      data
    );
  }

  delete(id) {
    return this.httpService.delete(
      this.mainUrl + this.deleteTaskUrl + `/${id}`
    );
  }
}

export default TodoService;
