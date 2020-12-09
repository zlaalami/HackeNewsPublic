"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var testing_2 = require("@angular/common/http/testing");
var stories_service_1 = require("./stories.service");
var app_config_1 = require("../config/app.config");
describe('StoriesService', function () {
    var storiesService;
    var httpMock;
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [
                testing_2.HttpClientTestingModule,
            ],
            providers: [
                stories_service_1.StoriesService
            ],
        });
        storiesService = testing_1.TestBed.get(stories_service_1.StoriesService);
        httpMock = testing_1.TestBed.get(testing_2.HttpTestingController);
    });
    it("should fetch stories as an Observable", testing_1.async(testing_1.inject([testing_2.HttpTestingController, stories_service_1.StoriesService], function (httpClient, storiesService) {
        var storyItem = [
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
            .subscribe(function (posts) {
            expect(posts.length).toBe(3);
        });
        var REST_API = app_config_1.appConfig.hackernewsAPI;
        var req = httpMock.expectOne("" + REST_API);
        expect(req.request.method).toBe("GET");
        req.flush(storyItem);
        httpMock.verify();
    })));
});
//# sourceMappingURL=stories.service.spec.js.map