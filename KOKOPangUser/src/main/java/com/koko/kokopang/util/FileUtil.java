package com.koko.kokopang.util;


import com.koko.kokopang.user.model.UserProfile;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;
import org.springframework.web.multipart.MultipartFile;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.UUID;

@Component
public class FileUtil {

    @Value("${file.path.upload-images}")
    private String uploadImagePath;

    public UserProfile storeImg(MultipartFile multipartFile, UserProfile profileImg) throws IOException {
        if (multipartFile == null) {
            return null;
        }
        if(multipartFile.isEmpty()) {
            return null;
        }

        String today = new SimpleDateFormat("yyMMdd").format(new Date());

        String saveFolder = uploadImagePath + File.separator + today + "_profile";
        File folder = new File(saveFolder);
        if (!folder.exists())
            folder.mkdirs();

        String originalFileName = multipartFile.getOriginalFilename();

        if (!originalFileName.contains(".")) {
            originalFileName += ".png";
        }

        if (!originalFileName.isEmpty()) {
            String saveFileName = UUID.randomUUID().toString()
                    + originalFileName.substring(originalFileName.lastIndexOf('.'));
            profileImg.setSaveFolder(today + "_profile");
            profileImg.setOriginalName(originalFileName);
            profileImg.setSaveName(saveFileName);

            // 파일 실제 저장
            multipartFile.transferTo(new File(folder, saveFileName));
        }

        return profileImg;
    }

    // 객체로 프로필 사진 삭제
    public void deleteFile(UserProfile profileImg) throws IOException {
        // 파일의 경로 생성
        String filePath = uploadImagePath + File.separator + profileImg.getSaveFolder() + File.separator + profileImg.getSaveName();
        Path path = Paths.get(filePath);

        // 파일 삭제
        Files.deleteIfExists(path);
    }

    // 아이디로 프로필 사진 삭제
    public void deleteProfileFile(UserProfile profileImg) throws IOException {
        // 파일의 경로 생성
        String filePath = uploadImagePath + File.separator + profileImg.getSaveFolder() + File.separator + profileImg.getSaveName();
        Path path = Paths.get(filePath);

        // 파일 삭제
        Files.deleteIfExists(path);
    }
}
