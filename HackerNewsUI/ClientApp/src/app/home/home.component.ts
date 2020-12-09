// <reference path="../../karma.conf.js" />
import { Component, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { StoriesService } from '../shared/stories.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  public hackerNewsStories: HackerNewsStories[];
  public pageOfStories: Array<any>;
  public counter: number = 1;
  public term: string;
  public show: boolean = true;
  config: any;

  public title: string;

  constructor(private storyService: StoriesService) {
    this.title = 'Hacker News Stories';
    this.postStories();
  }

  postStories() {
    let stories: any = this.storyService.getStories();

    stories.subscribe(result => {
      this.hackerNewsStories = result;
      this.hackerNewsStories.forEach((story) => {
        if (story !== null) {
          story.time = new Date(story.time * 1000).toLocaleDateString("en-US");
          //console.log(story);
          story.row = this.counter++;
        }
      });

      this.config = {
        itemsPerPage: 10,
        currentPage: 1,
        totalItems: this.hackerNewsStories.length
      };

    }, error => console.error(error));
  }

  pageChanged(event) {
    this.config.currentPage = event;
  }
}

interface HackerNewsStories {
  title: any;
  url: string;
  time: any;
  id: string;
  row: number;
}
