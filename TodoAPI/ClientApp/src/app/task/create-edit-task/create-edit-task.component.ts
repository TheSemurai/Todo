import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { TaskClient } from '../task.client';
import { EditCreateTask } from '../model/create-edit-task';
import { Router } from '@angular/router';

@Component({
  selector: 'create-edit-task',
  templateUrl: './create-edit-task.component.html',
})
export class CreateEditTaskComponent implements OnInit {
  creationTask: EditCreateTask = {
    title: '',
    content: '',
    isComplete: false,
  };

  constructor(private taskClient: TaskClient, private router: Router) {}

  ngOnInit() {}

  public toggleCheck() {
    //this.isComplete = !this.isComplete;
  }

  public onSubmit() {
    console.log('start submit');
    this.taskClient.createTask(this.creationTask);
  }

  public create() {
    console.log('start creataion');

    var request = this.taskClient.createTask(this.creationTask);

    // if (request) {
    //   this.router.navigate(['/tasks']);
    // }
    console.log('end creataion');

    //console.warn('something went wrong.');
  }

  updateTitle(title: string) {
    this.creationTask.title = title;
  }

  updateContent(content: string) {
    this.creationTask.content = content;
  }

  public routeBackward() {
    this.router.navigate(['/tasks']);
  }
}
