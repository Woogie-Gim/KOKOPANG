package com.koko.kokopanguser.repository;

import com.koko.kokopanguser.dto.RefreshToken;
import org.springframework.data.repository.CrudRepository;

public interface RefreshTokenRepository extends CrudRepository<RefreshToken, String> {
}
