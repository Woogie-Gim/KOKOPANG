package com.koko.kokopang.user.service;

import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.User;
import com.koko.kokopang.user.model.UserProfile;
import com.koko.kokopang.user.repository.UserRepository;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.ArrayList;
import java.util.List;

@Service
public class UserServiceImpl implements UserService {

    @Value("${file.request.path}")
    private String fileRequestPath;

    private final UserRepository userRepository;
    private final BCryptPasswordEncoder bCryptPasswordEncoder;
    private final UserProfileService userProfileService;

    public UserServiceImpl(UserRepository userRepository, BCryptPasswordEncoder bCryptPasswordEncoder, UserProfileService userProfileService) {
        this.userRepository = userRepository;
        this.bCryptPasswordEncoder = bCryptPasswordEncoder;
        this.userProfileService = userProfileService;
    }

    @Override
    public User signup(UserDTO userDTO) {
        String email = userDTO.getEmail();
        String password = userDTO.getPassword();

//        if (!email.contains("@") || email.contains("'")) {
//            return null;
//        }

        if (userRepository.existsByEmail(email)) {
            return null;
        }

        User newUser = new User();
        newUser.setEmail(email);
        newUser.setPassword(bCryptPasswordEncoder.encode(password));
        newUser.setName(userDTO.getName());
        newUser.setRating(1000);
        userRepository.save(newUser);

        return newUser;
    }

    @Override
    public UserDTO getProfile(String email) {
        User user = userRepository.findByEmail(email);
        UserDTO userDTO = new UserDTO();

        if (user == null) {
            System.out.println("User not exist");
            return null;
        }

        userDTO.setUserId(user.getId());
        userDTO.setName(user.getName());
        userDTO.setEmail(user.getEmail());
        userDTO.setRating(user.getRating());
        UserProfile img = userProfileService.getUserProfile(user.getId());
        if (img != null) {
            String imgUrl = fileRequestPath + "/profile/getImg/"
                    + img.getSaveFolder() + "/" + img.getOriginalName() + "/" + img.getSaveName();
            userDTO.setProfileImg(imgUrl);
        }
        return userDTO;
    }

    @Override
    @Transactional
    public User updateProfile(User user) {

        User userProfile = userRepository.findByEmail(user.getEmail());

        if (userProfile == null) {
            return null;
        }

        userProfile.setName(user.getName());

        return userProfile;
    }

    @Override
    @Transactional
    public boolean updatePassword(int userId, String password) {

        if (!password.contains("!") ||
                !password.contains("@") ||
                !password.contains("~") ||
                !password.contains("#") ||
                password.length() < 9 ||
                password.length() > 15
        ) {
            return false;
        }

        User user = userRepository.findById(userId);

        if (user == null) {
            return false;
        }

        user.setPassword(bCryptPasswordEncoder.encode(password));
        return true;
    }

    @Override
    public List<UserDTO> getAllUser() {
        List<User> allUsers = userRepository.findAllDesc();
        List<UserDTO> users = new ArrayList<>();

        for (User user:allUsers) {
            UserDTO reUser = new UserDTO();
            reUser.setUserId(user.getId());
            reUser.setName(user.getName());
            reUser.setEmail(user.getEmail());
            reUser.setRating(user.getRating());
            UserProfile img = userProfileService.getUserProfile(user.getId());
            if (img != null) {
                String imgUrl = fileRequestPath + "/profile/getImg/"
                        + img.getSaveFolder() + "/" + img.getOriginalName() + "/" + img.getSaveName();
                reUser.setProfileImg(imgUrl);
            }
            users.add(reUser);
        }

        return users;
    }
}
