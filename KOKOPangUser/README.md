
```
KOKOPangUser
├─ .gitignore
├─ gradle
│  └─ wrapper
│     ├─ gradle-wrapper.jar
│     └─ gradle-wrapper.properties
├─ gradlew
├─ gradlew.bat
└─ src
   ├─ main
   │  ├─ java
   │  │  └─ com
   │  │     └─ koko
   │  │        └─ kokopang
   │  │           ├─ board
   │  │           │  ├─ controller
   │  │           │  │  ├─ BoardController.java
   │  │           │  │  └─ CommentController.java
   │  │           │  ├─ dto
   │  │           │  │  ├─ BaseTimeEntity.java
   │  │           │  │  ├─ BoardDTO.java
   │  │           │  │  ├─ BoardListDTO.java
   │  │           │  │  ├─ CommentDTO.java
   │  │           │  │  └─ CommentListDTO.java
   │  │           │  ├─ model
   │  │           │  │  ├─ Board.java
   │  │           │  │  └─ Comment.java
   │  │           │  ├─ repository
   │  │           │  │  ├─ BoardRepository.java
   │  │           │  │  └─ CommentRepository.java
   │  │           │  └─ service
   │  │           │     ├─ BoardService.java
   │  │           │     ├─ BoardServiceImpl.java
   │  │           │     ├─ CommentService.java
   │  │           │     └─ CommentServiceImpl.java
   │  │           ├─ config
   │  │           │  ├─ RedisConfig.java
   │  │           │  └─ SecurityConfig.java
   │  │           ├─ item
   │  │           │  ├─ controller
   │  │           │  │  └─ ItemController.java
   │  │           │  ├─ dto
   │  │           │  │  └─ PointDTO.java
   │  │           │  ├─ model
   │  │           │  │  ├─ Coordinate.java
   │  │           │  │  └─ MapItemSpawn.java
   │  │           │  └─ service
   │  │           │     ├─ ItemService.java
   │  │           │     └─ ItemServiceImpl.java
   │  │           ├─ KokoPangUserApplication.java
   │  │           ├─ Rank
   │  │           │  ├─ controller
   │  │           │  │  └─ RankController.java
   │  │           │  └─ dto
   │  │           │     └─ RankDTO.java
   │  │           ├─ user
   │  │           │  ├─ controller
   │  │           │  │  ├─ FriendshipController.java
   │  │           │  │  ├─ ReissueController.java
   │  │           │  │  ├─ UserController.java
   │  │           │  │  └─ UserProfileController.java
   │  │           │  ├─ dto
   │  │           │  │  ├─ CustomUserDetails.java
   │  │           │  │  ├─ FriendDTO.java
   │  │           │  │  ├─ FriendshipDTO.java
   │  │           │  │  └─ UserDTO.java
   │  │           │  ├─ model
   │  │           │  │  ├─ Friendship.java
   │  │           │  │  ├─ User.java
   │  │           │  │  └─ UserProfile.java
   │  │           │  ├─ repository
   │  │           │  │  ├─ FriendshipRepository.java
   │  │           │  │  ├─ UserProfileRepository.java
   │  │           │  │  └─ UserRepository.java
   │  │           │  └─ service
   │  │           │     ├─ CustomUserDetailService.java
   │  │           │     ├─ FriendshipService.java
   │  │           │     ├─ FriendshipServiceImpl.java
   │  │           │     ├─ RedisService.java
   │  │           │     ├─ UserProfileService.java
   │  │           │     ├─ UserProfileServiceImpl.java
   │  │           │     ├─ UserService.java
   │  │           │     └─ UserServiceImpl.java
   │  │           └─ util
   │  │              ├─ FileUtil.java
   │  │              ├─ JWTFilter.java
   │  │              ├─ JWTUtil.java
   │  │              └─ LoginFilter.java
   │  └─ resources
   │     ├─ application.properties
   │     └─ keystore.p12
   └─ test
      └─ java
         └─ com
            └─ koko
               └─ kokopang
                  └─ KokoPangUserApplicationTests.java

```