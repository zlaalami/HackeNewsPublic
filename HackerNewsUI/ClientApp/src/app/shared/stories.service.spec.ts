import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StoriesService } from './stories.service';
import { appConfig } from '../config/app.config';

describe('StoriesService', () => {
  let storiesService: StoriesService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      providers: [
        StoriesService
      ],
    });

    storiesService = TestBed.get(StoriesService);
    httpMock = TestBed.get(HttpTestingController);
  });

  it(`should fetch stories as an Observable`, async(inject([HttpTestingController, StoriesService],
    (httpClient: HttpTestingController, storiesService: StoriesService) => {

      const storyItem = [
        {
          "id": 1,
          "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
          "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
        },
        {
          "id": 2,
          "title": "qui est esse",
          "body": "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
        },
        {
          "id": 3,
          "title": "ea molestias quasi exercitationem repellat qui ipsa sit aut",
          "body": "et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut"
        }
      ];

      storiesService.getStories()
        .subscribe((posts: any) => {
          expect(posts.length).toBe(3);
        });

      let REST_API = appConfig.hackernewsAPI;

      let req = httpMock.expectOne(`${REST_API}`);
      expect(req.request.method).toBe("GET");

      req.flush(storyItem);
      httpMock.verify();

    })));


});
