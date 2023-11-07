import { Component, OnInit } from '@angular/core';
import { TaskClient } from '../task.client';
import { EditCreateTask } from '../model/create-edit-task';

@Component({
  selector: 'create-edit-task',
  templateUrl: './create-edit-task.component.html',
})
export class CreateEditTaskComponent implements OnInit {
  title: string = '';
  content: string = '';
  isComplete: boolean = false;

  constructor(private taskClient: TaskClient) {}

  ngOnInit() {}

  public toggleCheck() {
    this.isComplete = !this.isComplete;
  }

  public create() {
    console.log('start creataion');

    const creationTask: EditCreateTask = {
      title: this.title,
      content: this.content,
      isComplete: this.isComplete,
    };
    this.taskClient.createTask(creationTask);
  }

  public back = () => {};
}
