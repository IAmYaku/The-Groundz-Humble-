//
//  AwesomeVideoFullscreenPlayer.swift
//  SAVideoPlayer
//
//  Created by Gabriel Coman on 06/02/2019.
//

import Foundation
import UIKit

class AwesomeVideoFullscreenPlayer: UIViewController {
    
    private weak var player: AwesomeVideoPlayer!
    private var isPlaying: Bool
    private var previousFrame: CGRect
    private var previousCentre: CGPoint
    
    override var prefersStatusBarHidden: Bool {
        return true
    }
    
    override var preferredStatusBarUpdateAnimation: UIStatusBarAnimation {
        return .fade
    }
    
    override var prefersHomeIndicatorAutoHidden: Bool {
        return true
    }
    
    init(withVideoPlayer player: AwesomeVideoPlayer,
         andIsCurrentlyPlaying playing: Bool) {
        self.player = player
        self.isPlaying = playing
        previousFrame = player.frame
        previousCentre = player.center
        super.init(nibName: nil, bundle: nil)
    }
    
    required init?(coder aDecoder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    private func setUp() {
        player.alpha = 0.0
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        view.addSubview(player)
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        if isPlaying {
            player.getAVPlayer()?.play()
        }
        UIView.animate(withDuration: 0.3, animations: {
            self.player.frame = CGRect(x: 0.0, y: 0.0, width: self.view.layoutMarginsGuide.layoutFrame.size.height, height: self.view.layoutMarginsGuide.layoutFrame.size.width)
            self.player.center = self.view.center
            self.player.transform = CGAffineTransform(rotationAngle: .pi / 2)
            self.player.alpha = 1.0
        }, completion: { _ in
            self.player.layoutSubviews()
        })
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(animated)
        UIView.animate(withDuration: 0.3, animations: {
            self.player.frame = CGRect(x: 0.0, y: 0.0, width: self.previousFrame.size.height, height: self.previousFrame.width)
            self.player.center = self.previousCentre
            self.player.transform = CGAffineTransform(rotationAngle: 0)
        }, completion: { _ in
            self.player.layoutSubviews()
        })
    }
}

extension UIView {
    var parentViewController: UIViewController? {
        var parentResponder: UIResponder? = self
        while parentResponder != nil {
            parentResponder = parentResponder!.next
            if let viewController = parentResponder as? UIViewController {
                return viewController
            }
        }
        return nil
    }
}
