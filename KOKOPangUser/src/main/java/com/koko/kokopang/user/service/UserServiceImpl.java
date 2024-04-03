package com.koko.kokopang.user.service;

import com.koko.kokopang.user.dto.RankUpDTO;
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
import java.util.regex.Matcher;
import java.util.regex.Pattern;

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

    private boolean isValidPassword(String password) {
        // 알파벳 포함 여부를 확인하는 정규 표현식
        Pattern alphabetPattern = Pattern.compile("[a-zA-Z]");
        Matcher alphabetMatcher = alphabetPattern.matcher(password);

        // 숫자 포함 여부를 확인하는 정규 표현식
        Pattern digitPattern = Pattern.compile("[0-9]");
        Matcher digitMatcher = digitPattern.matcher(password);

        // 특수 문자 포함 여부를 확인하는 정규 표현식
        Pattern specialCharacterPattern = Pattern.compile("[~`@!#$%^&*+=\\-\\[\\]\\\\';,/{}|\":<>?]");
        Matcher specialCharacterMatcher = specialCharacterPattern.matcher(password);

        // 비밀번호 길이가 8에서 15 사이인지 확인
        boolean validLength = (password.length() > 7 && password.length() < 16);

        // 모든 조건을 만족하는지 확인하여 반환
        return alphabetMatcher.find() && digitMatcher.find() && specialCharacterMatcher.find() && validLength;
    }

    @Override
    public User signup(UserDTO userDTO) {
        String email = userDTO.getEmail();
        String password = userDTO.getPassword();

//        if (!email.contains("@") || email.contains("'")) {
//            return null;
//        }
//        if (!isValidPassword(password)) {
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

    @Override
    @Transactional
    public void rankUp(RankUpDTO rankUpDTO) {
        User user = userRepository.findById(rankUpDTO.getUserId());
        user.setRating(Math.round((float) rankUpDTO.getScore() / 10) + user.getRating());
    }
}
